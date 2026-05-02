using Microsoft.AspNetCore.Mvc;
using TicketBookingSystem.CabinService.Dtos;
using TicketBookingSystem.CabinService.Service;

namespace TicketBookingSystem.CabinService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CabinTypesController : ControllerBase
{
    private readonly ICabinTypeService _service;

    public CabinTypesController(ICabinTypeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CabinTypeDto>>> GetAll()
    {
        var types = await _service.GetAllCabinTypesAsync();
        return Ok(types);
    }

    [HttpPost]
    public async Task<ActionResult<CabinTypeDto>> Create(CabinTypeDto cabinTypeDto)
    {
        var result = await _service.CreateCabinTypeAsync(cabinTypeDto);
        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteCabinTypeAsync(id);
        
        // Return 200 OK with message for easier testing
        return Ok(new { Message = $"Successfully deleted Cabin Type with ID: {id}" });
    }
}
