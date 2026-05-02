using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.CabinService.Data;
using TicketBookingSystem.CabinService.Models;

namespace TicketBookingSystem.CabinService.Repository;

public class CabinTypeRepository : ICabinTypeRepository
{
    private readonly CabinDbContext _context;

    public CabinTypeRepository(CabinDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CabinType>> GetAllCabinTypesAsync()
    {
        return await _context.CabinTypes
            .Include(ct => ct.Amenities)
            .Include(ct => ct.Images)
            .ToListAsync();
    }

    public async Task<CabinType?> GetCabinTypeByIdAsync(int id)
    {
        return await _context.CabinTypes.FindAsync(id);
    }

    public async Task AddCabinTypeAsync(CabinType cabinType)
    {
        await _context.CabinTypes.AddAsync(cabinType);
    }

    public async Task DeleteCabinTypeAsync(int id)
    {
        var type = await _context.CabinTypes.FindAsync(id);
        if (type != null)
        {
            _context.CabinTypes.Remove(type);
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
