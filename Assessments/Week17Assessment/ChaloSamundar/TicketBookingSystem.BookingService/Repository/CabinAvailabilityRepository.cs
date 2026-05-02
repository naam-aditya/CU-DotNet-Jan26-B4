using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.BookingService.Data;
using TicketBookingSystem.BookingService.Models;

namespace TicketBookingSystem.BookingService.Repository
{
    public class CabinAvailabilityRepository : ICabinAvailabilityRepository
    {
        private readonly BookingDbContext _context;
        public CabinAvailabilityRepository(BookingDbContext context) => _context = context;

        public async Task<CabinAvailability?> GetAsync(int tripId, int cabinTypeId)
            => await _context.CabinAvailabilities
                .FirstOrDefaultAsync(c => c.TripId == tripId && c.CabinTypeId == cabinTypeId);

        public async Task<List<CabinAvailability>> GetByTripAsync(int tripId)
            => await _context.CabinAvailabilities
                .Where(c => c.TripId == tripId)
                .ToListAsync();

        public async Task UpdateAsync(CabinAvailability cabin)
        {
            _context.CabinAvailabilities.Update(cabin);
            await _context.SaveChangesAsync();
        }
    }
}
