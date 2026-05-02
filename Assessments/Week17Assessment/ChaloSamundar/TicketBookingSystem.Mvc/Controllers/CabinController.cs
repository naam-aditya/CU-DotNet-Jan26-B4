using Microsoft.AspNetCore.Mvc;
using TicketBookingSystem.Mvc.Services;
using TicketBookingSystem.Mvc.ViewModels;

namespace TicketBookingSystem.Web.Controllers;

public class CabinController : Controller
{
    private readonly ICabinWebService _cabinService;

    public CabinController(ICabinWebService cabinService)
    {
        _cabinService = cabinService;
    }

    // Accepts both `?cruiseId=` and `?tripId=` so the link can come from either
    // the trip details page (which uses tripId) or any caller using cruiseId.
    public async Task<IActionResult> Index(int? cruiseId = null, int? tripId = null, int shipId = 1)
    {
        // Resolve the actual cruise/trip id with sensible fallback chain:
        //   1. explicit cruiseId / tripId in the query string
        //   2. previously-stored value in session (so refresh works)
        //   3. default to 1
        var resolvedId = cruiseId ?? tripId
            ?? (int.TryParse(HttpContext.Session.GetString("CruiseId"), out var fromSession) ? fromSession : 1);

        // Persist for the next leg of the flow.
        HttpContext.Session.SetString("CruiseId", resolvedId.ToString());

        var cabinTypes = await _cabinService.GetCabinTypesAsync();

        var viewModel = new CabinSelectionViewModel
        {
            CruiseId = resolvedId,
            ShipId = shipId,
            CabinTypes = cabinTypes
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Select(int cabinTypeId, int? cruiseId = null, int? tripId = null, int shipId = 1)
    {
        // Same fallback as Index — the form might post cruiseId or tripId, or neither.
        var resolvedId = cruiseId ?? tripId
            ?? (int.TryParse(HttpContext.Session.GetString("CruiseId"), out var fromSession) ? fromSession : 1);

        var cabinTypes = await _cabinService.GetCabinTypesAsync();
        var selectedCabin = cabinTypes.FirstOrDefault(c => c.Id == cabinTypeId);

        if (selectedCabin is null)
        {
            TempData["SelectedCabinMessage"] = "Selected cabin is not available. Please choose another.";
            return RedirectToAction(nameof(Index), new { cruiseId = resolvedId, shipId });
        }

        HttpContext.Session.SetString("CabinType", selectedCabin.Name);
        HttpContext.Session.SetString("CruiseId", resolvedId.ToString());

        // Forward selection to BookingService flow with the REAL cruise id.
        return RedirectToAction("Create", "Booking", new
        {
            cruiseId = resolvedId,
            shipId,
            cabinTypeId
        });
    }
}
