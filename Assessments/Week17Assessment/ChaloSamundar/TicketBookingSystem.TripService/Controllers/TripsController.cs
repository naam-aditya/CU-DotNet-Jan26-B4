using Microsoft.AspNetCore.Mvc;
using TicketBookingSystem.TripService.DTOs;
using TicketBookingSystem.TripService.Models;
using TicketBookingSystem.TripService.Services;
// FluentValidation auto-validates the [FromBody] DTO before this action runs.

namespace TicketBookingSystem.TripService.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TripsController : ControllerBase
    {
        private readonly ITripService _service;

        public TripsController(ITripService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trip>>> GetTrips()
        {
            return Ok(await _service.GetAllTripsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TripWithItineraryDto>> GetTrip(int id)
        {
            return Ok(await _service.GetTripByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<Trip>> PostTrip(CreateTripDto dto)
        {
            // FluentValidation has already validated 'dto' (auto-validation enabled in Program.cs).
            // If validation failed, ValidationException was thrown and handled by the middleware
            // — this action never runs in that case.
            var createdTrip = await _service.CreateTripAsync(dto);
            return CreatedAtAction(nameof(GetTrip), new { id = createdTrip.Id }, createdTrip);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrip(int id, Trip trip)
        {
            await _service.UpdateTripAsync(id, trip);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            await _service.DeleteTripAsync(id);
            return NoContent();
        }
    }
}