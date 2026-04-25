using AutoMapper;
using Serilog;
using TicketBookingSystem.ItineraryService.DTOs;
using TicketBookingSystem.ItineraryService.Exceptions;
using TicketBookingSystem.ItineraryService.Models;
using TicketBookingSystem.ItineraryService.Repositories;

namespace TicketBookingSystem.ItineraryService.Services
{
    public class ItineraryServices : IItineraryService
    {
        private readonly IItineraryRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ItineraryServices(IItineraryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = Log.ForContext<ItineraryServices>();
        }

        public async Task<ItineraryItemDto?> GetByIdAsync(int id)
        {
            _logger.Information("Fetching itinerary item with ID: {ItemId}", id);

            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                _logger.Warning("Itinerary item with ID {ItemId} not found", id);
                throw new ItineraryNotFoundException($"Itinerary item with ID {id} not found.");
            }

            return _mapper.Map<ItineraryItemDto>(item);
        }

        public async Task<List<ItineraryItemDto>> GetByTripIdAsync(int tripId)
        {
            _logger.Information("Fetching itinerary items for Trip ID: {TripId}", tripId);

            if (tripId <= 0)
                throw new InvalidItineraryException("Trip ID must be greater than 0.");

            var items = await _repository.GetByTripIdAsync(tripId);
            _logger.Information("Retrieved {Count} itinerary items for Trip ID: {TripId}", items.Count, tripId);

            return _mapper.Map<List<ItineraryItemDto>>(items);
        }

        public async Task<ItineraryTripSummaryDto> GetTripSummaryAsync(int tripId)
        {
            _logger.Information("Generating trip summary for Trip ID: {TripId}", tripId);

            if (tripId <= 0)
                throw new InvalidItineraryException("Trip ID must be greater than 0.");

            var items = await _repository.GetByTripIdAsync(tripId);
            if (!items.Any())
            {
                _logger.Warning("No itinerary items found for Trip ID: {TripId}", tripId);
                throw new ItineraryNotFoundException($"No itinerary items found for Trip ID {tripId}.");
            }

            var (atSeaDays, portDays) = await _repository.GetAtSeaAndPortDaysCountAsync(tripId);
            var locations = await _repository.GetUniqueLocationsByTripIdAsync(tripId);

            var summary = new ItineraryTripSummaryDto
            {
                TripId = tripId,
                TotalDays = items.Count,
                AtSeaDays = atSeaDays,
                PortDays = portDays,
                Locations = locations,
                Items = _mapper.Map<List<ItineraryItemDto>>(items)
            };

            _logger.Information("Trip summary generated: {TotalDays} days, {AtSeaDays} at sea, {PortDays} at port",
                summary.TotalDays, summary.AtSeaDays, summary.PortDays);

            return summary;
        }

        public async Task<ItineraryItemDto> CreateAsync(CreateItineraryItemDto dto)
        {
            _logger.Information("Creating new itinerary item for Trip ID: {TripId}, Day: {DayNumber}",
                dto.TripId, dto.DayNumber);

            // Check for duplicate
            var exists = await _repository.ExistsByTripIdAndDayNumberAsync(dto.TripId, dto.DayNumber);
            if (exists)
            {
                _logger.Warning("Duplicate itinerary entry attempted for Trip ID: {TripId}, Day: {DayNumber}",
                    dto.TripId, dto.DayNumber);
                throw new DuplicateItineraryException(
                    $"An itinerary item already exists for Trip {dto.TripId} on Day {dto.DayNumber}.");
            }

            var item = _mapper.Map<ItineraryItems>(dto);
            await _repository.AddAsync(item);
            await _repository.SaveChangesAsync();

            _logger.Information("Itinerary item created successfully with ID: {ItemId}", item.Id);

            return _mapper.Map<ItineraryItemDto>(item);
        }

        public async Task<ItineraryItemDto> UpdateAsync(UpdateItineraryItemDto dto)
        {
            _logger.Information("Updating itinerary item with ID: {ItemId}", dto.Id);

            var item = await _repository.GetByIdAsync(dto.Id);
            if (item == null)
            {
                _logger.Warning("Itinerary item with ID {ItemId} not found for update", dto.Id);
                throw new ItineraryNotFoundException($"Itinerary item with ID {dto.Id} not found.");
            }

            // Check for duplicate if day or trip changed
            if (item.TripId != dto.TripId || item.DayNumber != dto.DayNumber)
            {
                var exists = await _repository.ExistsByTripIdAndDayNumberAsync(dto.TripId, dto.DayNumber);
                if (exists)
                {
                    _logger.Warning("Duplicate itinerary entry attempted for Trip ID: {TripId}, Day: {DayNumber}",
                        dto.TripId, dto.DayNumber);
                    throw new DuplicateItineraryException(
                        $"An itinerary item already exists for Trip {dto.TripId} on Day {dto.DayNumber}.");
                }
            }

            _mapper.Map(dto, item);
            _repository.Update(item);
            await _repository.SaveChangesAsync();

            _logger.Information("Itinerary item with ID {ItemId} updated successfully", dto.Id);

            return _mapper.Map<ItineraryItemDto>(item);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.Information("Deleting itinerary item with ID: {ItemId}", id);

            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                _logger.Warning("Itinerary item with ID {ItemId} not found for deletion", id);
                throw new ItineraryNotFoundException($"Itinerary item with ID {id} not found.");
            }

            _repository.Remove(item);
            await _repository.SaveChangesAsync();

            _logger.Information("Itinerary item with ID {ItemId} deleted successfully", id);

            return true;
        }

        public async Task<List<ItineraryItemDto>> GetUpcomingAsync(int days = 7)
        {
            _logger.Information("Fetching upcoming itinerary items for the next {Days} days", days);

            var futureDate = DateTime.UtcNow.AddDays(days);
            var items = await _repository.FindAsync(x => x.Date >= DateTime.UtcNow && x.Date <= futureDate);

            var sortedItems = items.OrderBy(x => x.Date).ToList();
            _logger.Information("Retrieved {Count} upcoming itinerary items", sortedItems.Count);

            return _mapper.Map<List<ItineraryItemDto>>(sortedItems);
        }

        public async Task<List<string>> GetUniqueLocationsAsync(int tripId)
        {
            _logger.Information("Fetching unique locations for Trip ID: {TripId}", tripId);

            var locations = await _repository.GetUniqueLocationsByTripIdAsync(tripId);
            _logger.Information("Retrieved {Count} unique locations for Trip ID: {TripId}", locations.Count, tripId);

            return locations;
        }
    }
}
