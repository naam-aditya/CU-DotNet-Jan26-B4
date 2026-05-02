using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.TripService.Data;
using TicketBookingSystem.TripService.Models;

namespace TicketBookingSystem.TripService.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly TripDbContext _context;

        public TripRepository(TripDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Trip>> GetAllAsync()
        {
            return await _context.Trip.ToListAsync();
        }

        public async Task<Trip?> GetByIdAsync(int id)
        {
            return await _context.Trip.FindAsync(id);
        }

        public async Task AddAsync(Trip trip)
        {
            await _context.Trip.AddAsync(trip);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Trip trip)
        {
            _context.Entry(trip).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Trip trip)
        {
            _context.Trip.Remove(trip);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Trip.AnyAsync(t => t.Id == id);
        }
    }
}