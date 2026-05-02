using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.BookingService.Data;
using TicketBookingSystem.BookingService.Models;

namespace TicketBookingSystem.BookingService.Repository;

public class PassengerRepository : IPassengerRepository
{
    private readonly BookingDbContext _context;
    public PassengerRepository(BookingDbContext context)
    {
        _context = context;
    }

    public async Task AddPassengerAsync(Passenger passenger)
    {
        _context.Passengers.Add(passenger);
        await _context.SaveChangesAsync();
    }

    public async Task AddPassengersAsync(List<Passenger> passengers)
    {
        _context.Passengers.AddRange(passengers);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Passenger>> GetPassengersByBookingReferenceAsync(string bookingReference)
        => await _context.Passengers.Where(p => p.BookingReference == bookingReference).ToListAsync();
}
