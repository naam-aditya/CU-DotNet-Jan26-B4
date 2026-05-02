using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using TicketBookingSystem.Mvc.DTOs;
using TicketBookingSystem.Mvc.Services;
using TicketBookingSystem.Mvc.ViewModels;

namespace TicketBookingSystem.Mvc.Controllers;

public class BookingController : Controller
{
    private readonly IBookingApiClient _client;
    private readonly ITripApiClient _tripClient;
    private readonly ICabinWebService _cabinService;
    //private readonly CabinReservationApiClient _cabinReservation;
    private readonly ILogger<BookingController> _logger;

    public BookingController(
        IBookingApiClient client,
        ITripApiClient tripClient,
        ICabinWebService cabinService,
        //CabinReservationApiClient cabinReservation,
        ILogger<BookingController> logger)
    {
        _client = client;
        _tripClient = tripClient;
        _cabinService = cabinService;
        //_cabinReservation = cabinReservation;
        _logger = logger;
    }

    // ── Helper ──
    private string GetUserIdFromToken()
    {
        var token = HttpContext.Session.GetString("JWT");
        if (string.IsNullOrEmpty(token)) return "guest";
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);
        return jwt.Claims.FirstOrDefault(c =>
            c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
        )?.Value ?? "guest";
    }

    [HttpGet]
    public async Task<IActionResult> Create(int? cruiseId = null, int? shipId = null, int? cabinTypeId = null)
    {
        //var pendingJson = HttpContext.Session.GetString("PendingBooking");
        //if (!string.IsNullOrEmpty(pendingJson))
        //{
        //    HttpContext.Session.Remove("PendingBooking");
        //    var pending = JsonSerializer.Deserialize<BookingViewModel>(pendingJson);
        //    if (pending != null)
        //        return View(pending);   // show the pre-filled form immediately
        //}
        var sourcePort = HttpContext.Session.GetString("Source") ?? string.Empty;
        var destinationPort = HttpContext.Session.GetString("Destination") ?? string.Empty;
        var heading = HttpContext.Session.GetString("TripHeading") ?? string.Empty;
        var cabinType = HttpContext.Session.GetString("CabinType") ?? "NIL";

        if (!int.TryParse(HttpContext.Session.GetString("Nights"), out int nights)) nights = -1;
        if (!int.TryParse(HttpContext.Session.GetString("Price"), out int totalFare)) totalFare = -1;

        var booking = new BookingViewModel
        {
            BookingReference = Guid.NewGuid().ToString(),
            SourcePort = sourcePort,
            DestinationPort = destinationPort,
            SourcePortCode = "NIL",
            DestinationPortCode = "NIL",
            JourneyName = heading,
            CabinType = cabinType,
            Embarkation = DateTime.Now.AddDays(30),
            Disembarkation = DateTime.Now.AddDays(37),
            Nights = nights,
            CruisePrice = totalFare > 0 ? totalFare : 0,
            TotalFare = totalFare > 0 ? totalFare : 0,
        };

        if (cruiseId.HasValue)
        {
            var trip = await _tripClient.GetByIdAsync(cruiseId.Value);
            if (trip != null)
            {
                var fromPort = trip.Ports.Length > 0 ? trip.Ports[0] : booking.SourcePort;
                var toPort = trip.Ports.Length > 1 ? trip.Ports[^1] : fromPort;

                booking.SourcePort = fromPort;
                booking.DestinationPort = toPort;
                booking.SourcePortCode = fromPort.Length >= 3 ? fromPort[..3].ToUpperInvariant() : fromPort.ToUpperInvariant();
                booking.DestinationPortCode = toPort.Length >= 3 ? toPort[..3].ToUpperInvariant() : toPort.ToUpperInvariant();
                booking.JourneyName = trip.Heading ?? booking.JourneyName;
                booking.Embarkation = trip.StartDate;
                booking.Disembarkation = trip.EndDate;
                booking.Nights = trip.Nights;
                booking.CruisePrice = trip.Price;

                HttpContext.Session.SetString("Source", fromPort);
                HttpContext.Session.SetString("Destination", toPort);
                HttpContext.Session.SetString("TripHeading", trip.Heading ?? "");
                HttpContext.Session.SetString("Nights", trip.Nights.ToString());
                HttpContext.Session.SetString("Price", ((int)trip.Price).ToString());
                HttpContext.Session.SetString("CruiseId", cruiseId.Value.ToString());
            }
        }

        if (cabinTypeId.HasValue)
        {
            var cabinTypes = await _cabinService.GetCabinTypesAsync();
            var selected = cabinTypes.FirstOrDefault(c => c.Id == cabinTypeId.Value);
            if (selected != null)
            {
                booking.CabinType = selected.Name;
                booking.CabinPricePerPerson = selected.PricePerPerson;
                HttpContext.Session.SetString("CabinTypeId", cabinTypeId.Value.ToString());
            }
        }

        var passengerCount = booking.Passengers?.Count > 0 ? booking.Passengers.Count : 1;
        booking.TotalFare = booking.CruisePrice + (passengerCount * booking.CabinPricePerPerson);

        // ── Show availability warning ──
        //if (cruiseId.HasValue && cabinTypeId.HasValue)
        //{
        //    var avail = await _cabinReservation.GetAvailabilityAsync(cruiseId.Value, cabinTypeId.Value);
        //    ViewBag.AvailableCabins = avail.AvailableCabins;
        //    ViewBag.IsAvailable = avail.IsAvailable;
        //}

        return View(booking);
    }

    [HttpPost]
    public async Task<IActionResult> Create(BookingViewModel booking)
    {
        var token = HttpContext.Session.GetString("JWT");
        if (string.IsNullOrEmpty(token))
        {
            HttpContext.Session.SetString("PendingBooking", JsonSerializer.Serialize(booking));
            //var returnUrl = Url.Action("Create", "Booking");
            var returnUrl = Url.Action("ResumeBooking", "Booking");
            return RedirectToAction("Login", "Auth", new { returnUrl });
        }
            //return RedirectToAction("Login", "Auth");

        if (booking.Passengers == null || booking.Passengers.Count == 0)
        {
            ModelState.AddModelError(string.Empty,
                "Please add at least one passenger before confirming the booking.");
            return View(booking);
        }

        if (!ModelState.IsValid) return View(booking);

        var passengerCount = booking.Passengers.Count;
        booking.TotalFare = booking.CruisePrice + (passengerCount * booking.CabinPricePerPerson);
        var userId = GetUserIdFromToken();

        // ── Generate fresh booking reference ──
        var bookingRef = Guid.NewGuid().ToString();

        // ── Step 1: Reserve cabin ──
        //int.TryParse(HttpContext.Session.GetString("CruiseId"), out int cruiseId);
        //int.TryParse(HttpContext.Session.GetString("CabinTypeId"), out int cabinTypeId);

        //if (cruiseId > 0 && cabinTypeId > 0)
        //{
        //    var (reserved, reserveError) = await _cabinReservation.ReserveAsync(
        //        cruiseId, cabinTypeId, bookingRef);

        //    if (!reserved)
        //    {
        //        ModelState.AddModelError(string.Empty,
        //            $"Sorry, no cabins are available for your selected type. {reserveError}");
        //        return View(booking);
        //    }

        //    Console.WriteLine($"[BookingCreate] Cabin reserved for booking {bookingRef}");
        //}

        // ── Step 2: Create booking in BookingService ──
        var baseFare = booking.CruisePrice + (passengerCount * booking.CabinPricePerPerson);
        var convenienceFee = 0m;

        var dto = new BookingDto
        {
            BookingReference = bookingRef,
            UserId = userId,
            SourcePort = booking.SourcePort,
            DestinationPort = booking.DestinationPort,
            SourcePortCode = booking.SourcePortCode,
            DestinationPortCode = booking.DestinationPortCode,
            JourneyName = booking.JourneyName,
            CabinType = booking.CabinType,
            Embarkation = booking.Embarkation,
            Disembarkation = booking.Disembarkation,
            BookingDate = DateTime.Now,
            Nights = booking.Nights,
            BaseFare = baseFare,
            ConvenienceFee = convenienceFee,
            TotalFare = baseFare + convenienceFee,
            Passengers = booking.Passengers
        };

        BookingDto? created;
        try
        {
            created = await _client.CreateBookingAsync(dto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "BookingService call failed");

            // ── Release cabin if booking fails ──
            //await _cabinReservation.ReleaseAsync(bookingRef);

            //ModelState.AddModelError(string.Empty,
            //    $"Couldn't reach the booking service ({ex.Message}).");
            return View(booking);
        }

        if (created == null)
        {
            // ── Release cabin if booking rejected ──
            //await _cabinReservation.ReleaseAsync(bookingRef);

            ModelState.AddModelError(string.Empty,
                "Booking service rejected the request. Check BookingService logs.");
            return View(booking);
        }

        // ── Step 3: Store booking details for payment ──
        HttpContext.Session.SetString("BookingDetails", JsonSerializer.Serialize(dto));
        HttpContext.Session.SetString("BookingReference", bookingRef);

        var bookingIdForPayment = Math.Abs(created.BookingReference.GetHashCode());
        if (bookingIdForPayment == 0) bookingIdForPayment = 1;

        Console.WriteLine($"[BookingCreate] Booking created: {bookingRef}, userId: {userId}");

        return RedirectToAction("Index", "Payment", new
        {
            bookingId = bookingIdForPayment,
            amount = booking.TotalFare
        });
    }

    [HttpGet("booking/get/{reference}")]
    public async Task<IActionResult> Confirm(string reference)
    {
        CheckToken(nameof(Create));
        var booking = await _client.GetBookingByReferenceAsync(reference);
        if (booking == null) return BadRequest("Booking not found.");

        var data = JsonSerializer.Deserialize<BookingViewModel>(
            HttpContext.Session.GetString("BookingDetails")
            ?? throw new InvalidOperationException("No booking details in session."));

        var model = new BookingViewModel
        {
            BookingReference = data!.BookingReference,
            SourcePort = data!.SourcePort,
            DestinationPort = data!.DestinationPort,
            SourcePortCode = data!.SourcePortCode,
            DestinationPortCode = data!.DestinationPortCode,
            JourneyName = data!.JourneyName,
            CabinType = data!.CabinType,
            Embarkation = data!.Embarkation,
            Disembarkation = data!.Disembarkation,
            Nights = data!.Nights,
            TotalFare = data!.TotalFare,
            Passengers = data!.Passengers
        };

        return View(model);
    }

    private IActionResult CheckToken(string actionName)
    {
        var token = HttpContext.Session.GetString("JWT");
        if (string.IsNullOrEmpty(token))
            return RedirectToAction("Login", "Auth");
        return RedirectToAction(actionName);
    }

    [HttpGet]
    public async Task<IActionResult> MyBookings()
    {
        var token = HttpContext.Session.GetString("JWT");
        if (string.IsNullOrEmpty(token))
            return RedirectToAction("Login", "Auth");

        var userId = GetUserIdFromToken();
        var bookings = await _client.GetBookingsByUserIdAsync(userId);
        return View(bookings);
    }
    public async Task<IActionResult> ResumeBooking()
    {
        var data = HttpContext.Session.GetString("PendingBooking");

        if (string.IsNullOrEmpty(data))
            return RedirectToAction("Create");

        var booking = JsonSerializer.Deserialize<BookingViewModel>(data);

        // ✅ Important: clear after reading
        HttpContext.Session.Remove("PendingBooking");

        // 🔥 Re-call your existing Create POST method
        return await Create(booking);
    }
}