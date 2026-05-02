using Microsoft.AspNetCore.Mvc;
using TicketBookingSystem.Mvc.Services;
using TicketBookingSystem.Mvc.ViewModels;

namespace TicketBookingSystem.Mvc.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripApiClient _trips;

        public TripsController(ITripApiClient trips)
        {
            _trips = trips;
        }

        [HttpGet]
        public async Task<IActionResult> Index(TripSearchViewModel criteria)
        {
            ViewData["Criteria"] = criteria;
            var results = await _trips.SearchAsync(criteria);
            return View(results);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var trip = await _trips.GetByIdAsync(id);
            if (trip is null) return NotFound();
            HttpContext.Session.SetString("Source", trip.Ports[0]);
            HttpContext.Session.SetString("Destination", trip.Ports[^1]);
            HttpContext.Session.SetString("TripHeading", trip.Heading ?? "");
            HttpContext.Session.SetString("Price", $"{trip.Price}");
            HttpContext.Session.SetString("Nights", $"{trip.Nights}");
            return View(trip);
        }
    }
}