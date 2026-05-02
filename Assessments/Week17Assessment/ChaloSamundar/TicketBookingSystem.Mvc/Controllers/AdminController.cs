using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text.Json;
using TicketBookingSystem.Mvc.DTOs;
using TicketBookingSystem.Mvc.Services;
using TicketBookingSystem.Mvc.ViewModels;

namespace TicketBookingSystem.Mvc.Controllers
{
    public class AdminController : Controller
    {
        private readonly IBookingApiClient _bookingClient;
        private readonly ITripApiClient _tripClient;
        private readonly IHttpClientFactory _factory;
        private readonly IConfiguration _config;

        private static readonly JsonSerializerOptions JsonOpts = new() { PropertyNameCaseInsensitive = true };

        public AdminController(
            IBookingApiClient bookingClient,
            ITripApiClient tripClient,
            IHttpClientFactory factory,
            IConfiguration config)
        {
            _bookingClient = bookingClient;
            _tripClient = tripClient;
            _factory = factory;
            _config = config;
        }

        // ── Guard: only Admin role ──
        private IActionResult? GuardAdmin()
        {
            var token = HttpContext.Session.GetString("JWT");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login", "Auth");

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var role = jwt.Claims.FirstOrDefault(c =>
                c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
            )?.Value;

            if (role != "Admin") return RedirectToAction("Index", "Home");
            return null;
        }

        private void AttachAuth(HttpClient client)
        {
            var token = HttpContext.Session.GetString("JWT");
            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
        }

        private HttpClient AuthClient(string name = "auth")
        {
            var c = _factory.CreateClient(name);
            AttachAuth(c);
            return c;
        }

        // ══════════════════════════════════════════
        // DASHBOARD
        // ══════════════════════════════════════════
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (GuardAdmin() is { } r) return r;

            var trips = await _tripClient.SearchAsync(new TripSearchViewModel());
            var users = await GetUsersAsync();
            var bookings = await GetAllBookingsAsync();

            ViewBag.TotalTrips = trips.Count;
            ViewBag.TotalUsers = users.Count;
            ViewBag.TotalBookings = bookings.Count;
            ViewBag.TotalRevenue = bookings.Sum(b => b.TotalFare);
            ViewBag.RecentBookings = bookings.OrderByDescending(b => b.BookingDate).Take(5).ToList();

            return View();
        }

        // ══════════════════════════════════════════
        // USERS
        // ══════════════════════════════════════════
        [HttpGet]
        public async Task<IActionResult> Users()
        {
            if (GuardAdmin() is { } r) return r;
            var users = await GetUsersAsync();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (GuardAdmin() is { } r) return r;
            var client = AuthClient();
            await client.DeleteAsync($"{_config["ServiceUrls:AuthService"]}api/admin/users/{id}");
            TempData["Success"] = "User deleted.";
            return RedirectToAction(nameof(Users));
        }

        // ══════════════════════════════════════════
        // BOOKINGS
        // ══════════════════════════════════════════
        [HttpGet]
        public async Task<IActionResult> Bookings()
        {
            if (GuardAdmin() is { } r) return r;
            var bookings = await GetAllBookingsAsync();
            return View(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBooking(string reference)
        {
            if (GuardAdmin() is { } r) return r;
            var client = AuthClient("booking");
            await client.DeleteAsync($"{_config["ServiceUrls:BookingService"]}api/booking/{reference}");
            TempData["Success"] = "Booking deleted.";
            return RedirectToAction(nameof(Bookings));
        }

        // ══════════════════════════════════════════
        // TRIPS CRUD
        // ══════════════════════════════════════════
        [HttpGet]
        public async Task<IActionResult> Trips()
        {
            if (GuardAdmin() is { } r) return r;
            var trips = await _tripClient.SearchAsync(new TripSearchViewModel());
            return View(trips);
        }

        [HttpGet]
        public IActionResult CreateTrip()
        {
            if (GuardAdmin() is { } r) return r;
            return View(new TripViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrip(TripViewModel model)
        {
            if (GuardAdmin() is { } r) return r;
            if (!ModelState.IsValid) return View(model);

            var client = _factory.CreateClient("trip");
            AttachAuth(client);
            var res = await client.PostAsJsonAsync(
                $"{_config["ServiceUrls:TripService"]}api/trips", model);

            if (res.IsSuccessStatusCode)
            {
                TempData["Success"] = "Trip created successfully!";
                return RedirectToAction(nameof(Trips));
            }

            ModelState.AddModelError("", "Failed to create trip.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditTrip(int id)
        {
            if (GuardAdmin() is { } r) return r;
            var trip = await _tripClient.GetByIdAsync(id);
            if (trip == null) return NotFound();
            return View(trip);
        }

        [HttpPost]
        public async Task<IActionResult> EditTrip(int id, TripViewModel model)
        {
            if (GuardAdmin() is { } r) return r;
            if (!ModelState.IsValid) return View(model);

            var client = _factory.CreateClient("trip");
            AttachAuth(client);
            var res = await client.PutAsJsonAsync(
                $"{_config["ServiceUrls:TripService"]}api/trips/{id}", model);

            if (res.IsSuccessStatusCode)
            {
                TempData["Success"] = "Trip updated successfully!";
                return RedirectToAction(nameof(Trips));
            }

            ModelState.AddModelError("", "Failed to update trip.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            if (GuardAdmin() is { } r) return r;
            var client = _factory.CreateClient("trip");
            AttachAuth(client);
            await client.DeleteAsync($"{_config["ServiceUrls:TripService"]}api/trips/{id}");
            TempData["Success"] = "Trip deleted.";
            return RedirectToAction(nameof(Trips));
        }

        // ══════════════════════════════════════════
        // HELPERS
        // ══════════════════════════════════════════
        private async Task<List<AdminUserViewModel>> GetUsersAsync()
        {
            try
            {
                var client = AuthClient();
                var res = await client.GetAsync(
                    $"{_config["ServiceUrls:AuthService"]}api/admin/users");
                if (!res.IsSuccessStatusCode) return [];
                var json = await res.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<AdminUserViewModel>>(json, JsonOpts) ?? [];
            }
            catch { return []; }
        }

        private async Task<List<BookingDto>> GetAllBookingsAsync()
        {
            try
            {
                var client = AuthClient("booking");
                var res = await client.GetAsync(
                    $"{_config["ServiceUrls:BookingService"]}api/booking/all");
                if (!res.IsSuccessStatusCode) return [];
                var json = await res.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                var raw = doc.RootElement.TryGetProperty("data", out var d)
                    ? d.GetRawText() : doc.RootElement.GetRawText();
                return JsonSerializer.Deserialize<List<BookingDto>>(raw, JsonOpts) ?? [];
            }
            catch { return []; }
        }
        // GET: /Admin/Itinerary?tripId=1
        [HttpGet]
        public async Task<IActionResult> Itinerary(int tripId = 0)
        {
            if (GuardAdmin() is { } r) return r;

            // Load all trips for the dropdown
            var trips = await _tripClient.SearchAsync(new TripSearchViewModel());
            ViewBag.Trips = trips;
            ViewBag.SelectedTripId = tripId;

            var items = new List<ItineraryItemViewModel>();
            if (tripId > 0)
            {
                items = await GetItineraryByTripAsync(tripId);
                ViewBag.SelectedTrip = trips.FirstOrDefault(t => t.Id == tripId);
            }

            return View(items);
        }

        // POST: /Admin/AddItineraryItem
        [HttpPost]
        public async Task<IActionResult> AddItineraryItem(ItineraryItemViewModel model)
        {
            if (GuardAdmin() is { } r) return r;

            try
            {
                var client = _factory.CreateClient("itinerary");
                AttachAuth(client);
                var res = await client.PostAsJsonAsync(
                    $"{_config["ServiceUrls:ItineraryService"]}api/itinerary", model);

                TempData["Success"] = res.IsSuccessStatusCode
                    ? "Itinerary item added successfully!"
                    : "Failed to add itinerary item.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
            }

            return RedirectToAction(nameof(Itinerary), new { tripId = model.TripId });
        }

        // POST: /Admin/DeleteItineraryItem
        [HttpPost]
        public async Task<IActionResult> DeleteItineraryItem(int id, int tripId)
        {
            if (GuardAdmin() is { } r) return r;

            try
            {
                var client = _factory.CreateClient("itinerary");
                AttachAuth(client);
                await client.DeleteAsync(
                    $"{_config["ServiceUrls:ItineraryService"]}api/itinerary/{id}");
                TempData["Success"] = "Item deleted.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
            }

            return RedirectToAction(nameof(Itinerary), new { tripId });
        }

        // ── Helper ──
        private async Task<List<ItineraryItemViewModel>> GetItineraryByTripAsync(int tripId)
        {
            try
            {
                var client = _factory.CreateClient("itinerary");
                AttachAuth(client);
                var res = await client.GetAsync(
                    $"{_config["ServiceUrls:ItineraryService"]}api/itinerary/trip/{tripId}");
                if (!res.IsSuccessStatusCode) return [];
                var json = await res.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<
                    List<ItineraryItemViewModel>>(json, JsonOpts) ?? [];
            }
            catch { return []; }
        }
    }
}
