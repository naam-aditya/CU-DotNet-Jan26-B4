using TicketBookingSystem.ItineraryService.Models;

namespace TicketBookingSystem.ItineraryService.Services
{
    public interface IItineraryService
    {
        Task<List<ItineraryItems>> GetByTripIdAsync(int tripId);
        Task AddAsync(ItineraryItems item);
        Task DeleteAsync(int id);
    }
}
