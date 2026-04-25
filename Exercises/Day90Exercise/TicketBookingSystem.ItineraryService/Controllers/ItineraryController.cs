using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using TicketBookingSystem.ItineraryService.Common;
using TicketBookingSystem.ItineraryService.DTOs;
using TicketBookingSystem.ItineraryService.Exceptions;
using TicketBookingSystem.ItineraryService.Services;

namespace TicketBookingSystem.ItineraryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItineraryController : ControllerBase
    {
        private readonly IItineraryService _service;
        private readonly IValidator<CreateItineraryItemDto> _createValidator;
        private readonly IValidator<UpdateItineraryItemDto> _updateValidator;
        private readonly ILogger _logger;

        public ItineraryController(
            IItineraryService service,
            IValidator<CreateItineraryItemDto> createValidator,
            IValidator<UpdateItineraryItemDto> updateValidator)
        {
            _service = service;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _logger = Log.ForContext<ItineraryController>();
        }

        /// <summary>
        /// Get itinerary item by ID
        /// </summary>
        [HttpGet("{id}")]
        [Produces(typeof(ApiResponse<ItineraryItemDto>))]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var item = await _service.GetByIdAsync(id);
                return Ok(ApiResponse<ItineraryItemDto>.SuccessResponse(item, "Itinerary item retrieved successfully.", 200));
            }
            catch (ItineraryNotFoundException ex)
            {
                _logger.Warning(ex, "Itinerary item not found");
                return NotFound(ApiResponse<ItineraryItemDto>.FailureResponse(ex.Message, 404));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error retrieving itinerary item");
                return StatusCode(500, ApiResponse<ItineraryItemDto>.FailureResponse("An internal error occurred.", 500));
            }
        }

        /// <summary>
        /// Get all itinerary items for a trip
        /// </summary>
        [HttpGet("trip/{tripId}")]
        [Produces(typeof(ApiResponse<List<ItineraryItemDto>>))]
        public async Task<IActionResult> GetByTripId(int tripId)
        {
            try
            {
                var items = await _service.GetByTripIdAsync(tripId);
                return Ok(ApiResponse<List<ItineraryItemDto>>.SuccessResponse(
                    items, $"Retrieved {items.Count} itinerary items.", 200));
            }
            catch (InvalidItineraryException ex)
            {
                _logger.Warning(ex, "Invalid trip ID");
                return BadRequest(ApiResponse<List<ItineraryItemDto>>.FailureResponse(ex.Message, 400));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error retrieving itinerary items");
                return StatusCode(500, ApiResponse<List<ItineraryItemDto>>.FailureResponse("An internal error occurred.", 500));
            }
        }

        /// <summary>
        /// Get trip summary with statistics
        /// </summary>
        [HttpGet("trip/{tripId}/summary")]
        [Produces(typeof(ApiResponse<ItineraryTripSummaryDto>))]
        public async Task<IActionResult> GetTripSummary(int tripId)
        {
            try
            {
                var summary = await _service.GetTripSummaryAsync(tripId);
                return Ok(ApiResponse<ItineraryTripSummaryDto>.SuccessResponse(summary, "Trip summary retrieved successfully.", 200));
            }
            catch (InvalidItineraryException ex)
            {
                _logger.Warning(ex, "Invalid trip ID for summary");
                return BadRequest(ApiResponse<ItineraryTripSummaryDto>.FailureResponse(ex.Message, 400));
            }
            catch (ItineraryNotFoundException ex)
            {
                _logger.Warning(ex, "No itinerary found for trip");
                return NotFound(ApiResponse<ItineraryTripSummaryDto>.FailureResponse(ex.Message, 404));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error retrieving trip summary");
                return StatusCode(500, ApiResponse<ItineraryTripSummaryDto>.FailureResponse("An internal error occurred.", 500));
            }
        }

        /// <summary>
        /// Create a new itinerary item
        /// </summary>
        [HttpPost]
        [Produces(typeof(ApiResponse<ItineraryItemDto>))]
        public async Task<IActionResult> Create([FromBody] CreateItineraryItemDto dto)
        {
            try
            {
                // Validate DTO
                var validationResult = await _createValidator.ValidateAsync(dto);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    _logger.Warning("Validation failed for create itinerary item: {Errors}", string.Join(", ", errors));
                    return BadRequest(ApiResponse<ItineraryItemDto>.FailureResponse(
                        "Validation failed.", 400, errors));
                }

                var item = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = item.Id },
                    ApiResponse<ItineraryItemDto>.SuccessResponse(item, "Itinerary item created successfully.", 201));
            }
            catch (DuplicateItineraryException ex)
            {
                _logger.Warning(ex, "Duplicate itinerary entry attempted");
                return Conflict(ApiResponse<ItineraryItemDto>.FailureResponse(ex.Message, 409));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error creating itinerary item");
                return StatusCode(500, ApiResponse<ItineraryItemDto>.FailureResponse("An internal error occurred.", 500));
            }
        }

        /// <summary>
        /// Update an itinerary item
        /// </summary>
        [HttpPut("{id}")]
        [Produces(typeof(ApiResponse<ItineraryItemDto>))]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateItineraryItemDto dto)
        {
            try
            {
                if (id != dto.Id)
                {
                    _logger.Warning("ID mismatch in update request: {PathId} vs {BodyId}", id, dto.Id);
                    return BadRequest(ApiResponse<ItineraryItemDto>.FailureResponse(
                        "ID in URL does not match ID in request body.", 400));
                }

                // Validate DTO
                var validationResult = await _updateValidator.ValidateAsync(dto);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    _logger.Warning("Validation failed for update itinerary item: {Errors}", string.Join(", ", errors));
                    return BadRequest(ApiResponse<ItineraryItemDto>.FailureResponse(
                        "Validation failed.", 400, errors));
                }

                var item = await _service.UpdateAsync(dto);
                return Ok(ApiResponse<ItineraryItemDto>.SuccessResponse(item, "Itinerary item updated successfully.", 200));
            }
            catch (ItineraryNotFoundException ex)
            {
                _logger.Warning(ex, "Itinerary item not found for update");
                return NotFound(ApiResponse<ItineraryItemDto>.FailureResponse(ex.Message, 404));
            }
            catch (DuplicateItineraryException ex)
            {
                _logger.Warning(ex, "Duplicate itinerary entry attempted during update");
                return Conflict(ApiResponse<ItineraryItemDto>.FailureResponse(ex.Message, 409));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating itinerary item");
                return StatusCode(500, ApiResponse<ItineraryItemDto>.FailureResponse("An internal error occurred.", 500));
            }
        }

        /// <summary>
        /// Delete an itinerary item
        /// </summary>
        [HttpDelete("{id}")]
        [Produces(typeof(ApiResponse))]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok(ApiResponse.SuccessResponse("Itinerary item deleted successfully.", 200));
            }
            catch (ItineraryNotFoundException ex)
            {
                _logger.Warning(ex, "Itinerary item not found for deletion");
                return NotFound(ApiResponse.FailureResponse(ex.Message, 404));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error deleting itinerary item");
                return StatusCode(500, ApiResponse.FailureResponse("An internal error occurred.", 500));
            }
        }

        /// <summary>
        /// Get upcoming itinerary items within specified days
        /// </summary>
        [HttpGet("upcoming")]
        [Produces(typeof(ApiResponse<List<ItineraryItemDto>>))]
        public async Task<IActionResult> GetUpcoming([FromQuery] int days = 7)
        {
            try
            {
                var items = await _service.GetUpcomingAsync(days);
                return Ok(ApiResponse<List<ItineraryItemDto>>.SuccessResponse(
                    items, $"Retrieved {items.Count} upcoming itinerary items.", 200));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error retrieving upcoming itinerary items");
                return StatusCode(500, ApiResponse<List<ItineraryItemDto>>.FailureResponse("An internal error occurred.", 500));
            }
        }

        /// <summary>
        /// Get unique locations for a trip
        /// </summary>
        [HttpGet("trip/{tripId}/locations")]
        [Produces(typeof(ApiResponse<List<string>>))]
        public async Task<IActionResult> GetLocations(int tripId)
        {
            try
            {
                var locations = await _service.GetUniqueLocationsAsync(tripId);
                return Ok(ApiResponse<List<string>>.SuccessResponse(
                    locations, $"Retrieved {locations.Count} unique locations.", 200));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error retrieving locations");
                return StatusCode(500, ApiResponse<List<string>>.FailureResponse("An internal error occurred.", 500));
            }
        }
    }
}
