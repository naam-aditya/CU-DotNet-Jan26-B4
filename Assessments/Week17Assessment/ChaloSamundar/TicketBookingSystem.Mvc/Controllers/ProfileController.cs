using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using TicketBookingSystem.Mvc.Services;
using TicketBookingSystem.Mvc.ViewModels;

namespace TicketBookingSystem.Mvc.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IBookingApiClient _bookingClient;
        private readonly ProfileApiClient _profileClient;

        public ProfileController(IBookingApiClient bookingClient, ProfileApiClient profileClient)
        {
            _bookingClient = bookingClient;
            _profileClient = profileClient;
        }

        // ── Helper ──
        private string GetUserIdFromToken()
        {
            var token = HttpContext.Session.GetString("JWT");
            if (string.IsNullOrEmpty(token)) return string.Empty;
            var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            return jwt.Claims.FirstOrDefault(c =>
                c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
            )?.Value ?? string.Empty;
        }

        // GET: /Profile/Index
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JWT");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            // Load from AuthService DB — persists across sessions
            var model = await _profileClient.GetProfileAsync()
                        ?? new UserProfileViewModel();

            return View(model);
        }

        // POST: /Profile/Index — save to AuthService DB
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(UserProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var success = await _profileClient.SaveProfileAsync(model);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Failed to save profile. Please try again.");
                return View(model);
            }

            model.IsSaved = true;
            TempData["Success"] = "Profile saved successfully!";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Profile/Edit
        public async Task<IActionResult> Edit()
        {
            var token = HttpContext.Session.GetString("JWT");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var model = await _profileClient.GetProfileAsync()
                        ?? new UserProfileViewModel();
            ViewBag.EditMode = true;
            return View("Index", model);
        }

        // POST: /Profile/Delete — clears profile fields
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete()
        {
            await _profileClient.SaveProfileAsync(new UserProfileViewModel());
            TempData["Success"] = "Profile cleared.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Profile/MyBookings
        public async Task<IActionResult> MyBookings()
        {
            var token = HttpContext.Session.GetString("JWT");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var userId = GetUserIdFromToken();
            Console.WriteLine($"[ProfileController.MyBookings] userId = '{userId}'");

            var bookings = await _bookingClient.GetBookingsByUserIdAsync(userId);
            Console.WriteLine($"[ProfileController.MyBookings] bookings count = {bookings.Count}");

            return View(bookings);
        }
    }
}
