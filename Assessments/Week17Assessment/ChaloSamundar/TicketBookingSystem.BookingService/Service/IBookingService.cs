using TicketBookingSystem.BookingService.DTOs;

namespace TicketBookingSystem.BookingService.Service
{
    public interface IBookingService
    {
        Task<BookingResponseDto> CreateBooking(BookingRequestDto booking);
        Task<BookingApiResponse<BookingResponseDto>> GetBookingByReference(string bookingReference);

        Task<BookingApiResponse<List<BookingResponseDto>>> GetBookingsByUserIdAsync(string userId); // ← new

        Task<BookingApiResponse<List<BookingResponseDto>>> GetAllBookingsAsync();
    }
}
