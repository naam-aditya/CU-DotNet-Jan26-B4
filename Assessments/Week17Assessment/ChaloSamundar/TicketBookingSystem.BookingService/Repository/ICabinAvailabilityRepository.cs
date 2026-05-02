using TicketBookingSystem.BookingService.Models;

namespace TicketBookingSystem.BookingService.Repository
{
    public interface ICabinAvailabilityRepository
    {
        Task<CabinAvailability?> GetAsync(int tripId, int cabinTypeId);
        Task<List<CabinAvailability>> GetByTripAsync(int tripId);
        Task UpdateAsync(CabinAvailability cabin);
    }
}
