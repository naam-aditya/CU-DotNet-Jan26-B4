using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.CabinService.Data;
using TicketBookingSystem.CabinService.Models;

namespace TicketBookingSystem.CabinService.Repository;

public class CabinRepository : ICabinRepository
{
    private readonly CabinDbContext _context;

    public CabinRepository(CabinDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cabin>> GetCabinsByShipIdAsync(int shipId)
    {
        return await _context.Cabins
            .Where(c => c.ShipId == shipId)
            .Include(c => c.CabinType)
            .ToListAsync();
    }

    public async Task<Cabin?> GetCabinByIdAsync(int id)
    {
        return await _context.Cabins.Include(c => c.CabinType).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task UpdateCabinAsync(Cabin cabin)
    {
        _context.Entry(cabin).State = EntityState.Modified;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
