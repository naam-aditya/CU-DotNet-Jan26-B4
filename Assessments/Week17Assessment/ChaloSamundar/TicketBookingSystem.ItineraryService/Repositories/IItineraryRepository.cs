using TicketBookingSystem.ItineraryService.Models;

namespace TicketBookingSystem.ItineraryService.Repositories
{
    public interface IItineraryRepository
    {
        Task<List<ItineraryItems>> GetByTripIdAsync(int tripId);
        Task AddAsync(ItineraryItems item);
        Task<IEnumerable<ItineraryItems>> GetAllAsync();
        Task<ItineraryItems?> GetByIdAsync(int id);
        Task DeleteAsync(ItineraryItems item);
    }
}
