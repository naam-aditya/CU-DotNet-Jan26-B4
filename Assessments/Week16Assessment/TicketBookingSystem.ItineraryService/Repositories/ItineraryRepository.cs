using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.ItineraryService.Data;
using TicketBookingSystem.ItineraryService.Models;

namespace TicketBookingSystem.ItineraryService.Repositories
{
    public class ItineraryRepository : GenericRepository<ItineraryItems>, IItineraryRepository
    {
        public ItineraryRepository(AppDbContext context) : base(context) { }

        public async Task<List<ItineraryItems>> GetByTripIdAsync(int tripId)
        {
            return await _dbSet
                .Where(x => x.TripId == tripId)
                .OrderBy(x => x.DayNumber)
                .ToListAsync();
        }

        public async Task<ItineraryItems?> GetByTripIdAndDayNumberAsync(int tripId, int dayNumber)
        {
            return await _dbSet
                .FirstOrDefaultAsync(x => x.TripId == tripId && x.DayNumber == dayNumber);
        }

        public async Task<int> CountByTripIdAsync(int tripId)
        {
            return await _dbSet
                .Where(x => x.TripId == tripId)
                .CountAsync();
        }

        public async Task<bool> ExistsByTripIdAndDayNumberAsync(int tripId, int dayNumber)
        {
            return await _dbSet
                .AnyAsync(x => x.TripId == tripId && x.DayNumber == dayNumber);
        }

        public async Task<List<string>> GetUniqueLocationsByTripIdAsync(int tripId)
        {
            return await _dbSet
                .Where(x => x.TripId == tripId)
                .Select(x => x.Location)
                .Distinct()
                .ToListAsync();
        }

        public async Task<(int AtSeaDays, int PortDays)> GetAtSeaAndPortDaysCountAsync(int tripId)
        {
            var items = await _dbSet
                .Where(x => x.TripId == tripId)
                .ToListAsync();

            var atSeaDays = items.Count(x => x.IsAtSea);
            var portDays = items.Count(x => !x.IsAtSea);

            return (atSeaDays, portDays);
        }
    }
}
