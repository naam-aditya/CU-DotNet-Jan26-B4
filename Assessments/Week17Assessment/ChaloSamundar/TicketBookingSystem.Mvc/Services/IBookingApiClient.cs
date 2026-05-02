using TicketBookingSystem.Mvc.DTOs;

namespace TicketBookingSystem.Mvc.Services;

public interface IBookingApiClient
{
        Task<BookingDto?> CreateBookingAsync(BookingDto booking);
        Task<BookingDto?> GetBookingByReferenceAsync(string bookingReference);

    //new
    Task<List<BookingDto>> GetBookingsByUserIdAsync(string userId);
}