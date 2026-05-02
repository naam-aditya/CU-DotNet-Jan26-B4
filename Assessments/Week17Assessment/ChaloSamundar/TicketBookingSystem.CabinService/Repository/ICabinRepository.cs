using TicketBookingSystem.CabinService.Models;

namespace TicketBookingSystem.CabinService.Repository;

public interface ICabinRepository
{
    Task<IEnumerable<Cabin>> GetCabinsByShipIdAsync(int shipId);
    Task<Cabin?> GetCabinByIdAsync(int id);
    Task UpdateCabinAsync(Cabin cabin);
    Task SaveChangesAsync();
}
