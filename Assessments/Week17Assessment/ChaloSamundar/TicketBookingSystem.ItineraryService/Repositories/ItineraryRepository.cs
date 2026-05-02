using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.ItineraryService.Data;
using TicketBookingSystem.ItineraryService.Models;

namespace TicketBookingSystem.ItineraryService.Repositories
{
    public class ItineraryRepository : IItineraryRepository
    {
        private readonly AppDbContext _context;

        public ItineraryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ItineraryItems>> GetByTripIdAsync(int tripId)
        {
            return await _context.ItineraryItems
                .Where(x => x.TripId == tripId)
                .OrderBy(x => x.DayNumber)
                .ToListAsync();
        }

        public async Task AddAsync(ItineraryItems item)
        {
            _context.ItineraryItems.Add(item);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<ItineraryItems>> GetAllAsync()
        {
            return await _context.ItineraryItems.ToListAsync(); 
        }
        public async Task<ItineraryItems?> GetByIdAsync(int id)
     => await _context.ItineraryItems.FindAsync(id);

        public async Task DeleteAsync(ItineraryItems item)
        {
            _context.ItineraryItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
