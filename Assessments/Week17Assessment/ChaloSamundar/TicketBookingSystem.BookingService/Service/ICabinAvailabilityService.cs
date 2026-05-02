using TicketBookingSystem.BookingService.DTOs;

namespace TicketBookingSystem.BookingService.Service
{
    public interface ICabinAvailabilityService
    {
        Task<bool> IsAvailableAsync(int tripId, int cabinTypeId);
        Task<int> GetAvailableCountAsync(int tripId, int cabinTypeId);
        Task<bool> DecrementAsync(int tripId, int cabinTypeId);   // called when booking confirmed
        Task<bool> IncrementAsync(int tripId, int cabinTypeId);   // called when booking cancelled
        Task<List<CabinAvailabilityDto>> GetAllForTripAsync(int tripId);
    }
}
