using TicketBookingSystem.CabinService.Models;

namespace TicketBookingSystem.CabinService.Repository;

public interface ICabinTypeRepository
{
    Task<IEnumerable<CabinType>> GetAllCabinTypesAsync();
    Task<CabinType?> GetCabinTypeByIdAsync(int id);
    Task AddCabinTypeAsync(CabinType cabinType);
    Task DeleteCabinTypeAsync(int id);
    Task SaveChangesAsync();
}
