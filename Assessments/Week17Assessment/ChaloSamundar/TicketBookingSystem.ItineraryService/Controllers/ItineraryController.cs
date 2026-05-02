using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketBookingSystem.ItineraryService.Models;
using TicketBookingSystem.ItineraryService.Services;

namespace TicketBookingSystem.ItineraryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItineraryController : ControllerBase
    {
        private readonly IItineraryService _service;

        public ItineraryController(IItineraryService service)
        {
            _service = service;
        }

        [HttpGet("trip/{tripId}")]
        public async Task<ActionResult<IEnumerable<ItineraryItems>>> GetByTripId(int tripId)
        {
            var items = await _service.GetByTripIdAsync(tripId);
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItineraryItems item)
        {
            await _service.AddAsync(item);
            return Ok(item);
        }
        // ── NEW: DELETE api/itinerary/{id} ──
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
