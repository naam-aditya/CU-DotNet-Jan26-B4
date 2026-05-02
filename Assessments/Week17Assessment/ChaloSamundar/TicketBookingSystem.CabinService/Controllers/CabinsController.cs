using Microsoft.AspNetCore.Mvc;
using TicketBookingSystem.CabinService.Dtos;
using TicketBookingSystem.CabinService.Service;

namespace TicketBookingSystem.CabinService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CabinsController : ControllerBase
{
    private readonly ICabinService _service;

    public CabinsController(ICabinService service)
    {
        _service = service;
    }

    [HttpGet("Ship/{shipId}")]
    public async Task<ActionResult<IEnumerable<CabinDto>>> GetByShip(int shipId)
    {
        var cabins = await _service.GetCabinsByShipIdAsync(shipId);
        return Ok(cabins);
    }

    /// <summary>
    /// Checks the current availability of a specific cabin.
    /// </summary>
    [HttpGet("{id}/availability")]
    public async Task<ActionResult<bool>> CheckAvailability(int id)
    {
        var cabin = await _service.GetCabinByIdAsync(id);
        return Ok(cabin.IsAvailable);
    }

    /// <summary>
    /// Updates availability and returns the updated cabin object (200 OK).
    /// </summary>
    [HttpPut("{id}/availability")]
    public async Task<ActionResult<CabinDto>> UpdateAvailability(int id, [FromBody] bool isAvailable)
    {
        var updatedCabin = await _service.UpdateCabinAvailabilityAsync(id, isAvailable);
        return Ok(updatedCabin);
    }
}
