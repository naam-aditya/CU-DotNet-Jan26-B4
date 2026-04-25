using TicketBookingSystem.ItineraryService.DTOs;

namespace TicketBookingSystem.ItineraryService.Services
{
    public interface IItineraryService
    {
        Task<ItineraryItemDto?> GetByIdAsync(int id);
        Task<List<ItineraryItemDto>> GetByTripIdAsync(int tripId);
        Task<ItineraryTripSummaryDto> GetTripSummaryAsync(int tripId);
        Task<ItineraryItemDto> CreateAsync(CreateItineraryItemDto dto);
        Task<ItineraryItemDto> UpdateAsync(UpdateItineraryItemDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<ItineraryItemDto>> GetUpcomingAsync(int days = 7);
        Task<List<string>> GetUniqueLocationsAsync(int tripId);
    }
}

