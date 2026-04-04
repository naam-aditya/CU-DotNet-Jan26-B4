using Microsoft.AspNetCore.Mvc;
using Vagabond.TravelDestinationService.Dtos;
using Vagabond.TravelDestinationService.Models;
using Vagabond.TravelDestinationService.Services;

namespace Vagabond.TravelDestinationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationsController : ControllerBase
    {
        private readonly IDestinationService _service;

        public DestinationsController(IDestinationService service)
        {
            _service = service;
        }

        // GET: api/Destinations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Destination>>> GetDestination()
            => Ok(await _service.GetAllAsync());

        // GET: api/Destinations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Destination>> GetDestination(int id)
        {
            var destination = await _service.GetByIdAsync(id);

            if (destination == null)
                return NotFound("No such destination exists");

            return destination;
        }

        // PUT: api/Destinations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDestination(int id, Destination destination)
        {
            if (id != destination.Id)
                return BadRequest();
            
            var message= await _service.UpdateAsync(destination);
            return Ok(message);
        }

        // POST: api/Destinations
        [HttpPost]
        public async Task<ActionResult<Destination>> PostDestination(CreateDestinationDto dto)
        {
            var message = await _service.AddAsync(dto);
            return CreatedAtAction("GetDestination", message);
        }

        // DELETE: api/Destinations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDestination(int id)
        {
            var message = await _service.DeleteAsync(id);
            return Ok(message);
        }
    }
}
