using TicketBookingSystem.ItineraryService.Models;

namespace TicketBookingSystem.ItineraryService.Repositories
{
    public interface IItineraryRepository : IGenericRepository<ItineraryItems>
    {
        Task<List<ItineraryItems>> GetByTripIdAsync(int tripId);
        Task<ItineraryItems?> GetByTripIdAndDayNumberAsync(int tripId, int dayNumber);
        Task<int> CountByTripIdAsync(int tripId);
        Task<bool> ExistsByTripIdAndDayNumberAsync(int tripId, int dayNumber);
        Task<List<string>> GetUniqueLocationsByTripIdAsync(int tripId);
        Task<(int AtSeaDays, int PortDays)> GetAtSeaAndPortDaysCountAsync(int tripId);
    }
}

