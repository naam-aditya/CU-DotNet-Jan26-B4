using Microsoft.EntityFrameworkCore;
using Vagabond.TravelDestinationService.Data;
using Vagabond.TravelDestinationService.Models;
using Vagabond.TravelDestinationService.Repositories;
using Vagabond.TravelDestinationService.Exceptions;

namespace Vagabond.TravelDestinationService
{
    public class DestinationRepository : IDestinationRepository
    {
        private readonly DestinationDbContext _context;
        public DestinationRepository(DestinationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Destination destination)
        {
            _context.Destination.Add(destination);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var destination = await GetByIdAsync(id) 
                ?? throw new DestinationNotFoundException("Destination does not exists.");
            
            _context.Destination.Remove(destination);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Destination>> GetAllAsync() =>
            await _context.Destination.ToListAsync();

        public async Task<Destination> GetByIdAsync(int id) =>
            await _context.Destination.FindAsync(id) ?? throw new DestinationNotFoundException("No such destination exists");

        public async Task UpdateAsync(Destination destination)
        {
            var data = await GetByIdAsync(destination.Id)
                ?? throw new DestinationNotFoundException("No such destination found.");

            data.Replace(destination);
            await _context.SaveChangesAsync();
        }
    }
}
