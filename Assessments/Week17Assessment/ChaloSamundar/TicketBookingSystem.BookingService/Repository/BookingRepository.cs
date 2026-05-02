using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.BookingService.Data;
using TicketBookingSystem.BookingService.Models;

namespace TicketBookingSystem.BookingService.Repository;

public class BookingRepository : IBookingRepository
{
    private readonly BookingDbContext _context;

    public BookingRepository(BookingDbContext context)
    {
        _context = context;
    }

    public async Task CreateBooking(Booking booking)
    {

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();
    }

    public async Task<Booking?> GetBookingByReference(string bookingReference)
        => await _context.Bookings.FirstOrDefaultAsync(b => b.BookingReference == bookingReference);
    //new
    public async Task<List<Booking>> GetBookingsByUserIdAsync(string userId)
       => await _context.Bookings
           .Where(b => b.UserId == userId)
           .OrderByDescending(b => b.BookingDate)
           .ToListAsync();
    public async Task<List<Booking>> GetAllBookingsAsync()
    => await _context.Bookings.OrderByDescending(b => b.BookingDate).ToListAsync();
}
