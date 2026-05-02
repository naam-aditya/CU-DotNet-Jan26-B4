using TicketBookingSystem.BookingService.Models;

namespace TicketBookingSystem.BookingService.Repository
{
    public interface IBookingRepository
    {
        Task CreateBooking(Booking booking);
        Task<Booking?> GetBookingByReference(string bookingReference);
        Task<List<Booking>> GetBookingsByUserIdAsync(string userId);  // ← new
        Task<List<Booking>> GetAllBookingsAsync();
    }
}
