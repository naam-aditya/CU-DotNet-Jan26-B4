using TicketBookingSystem.CabinService.Dtos;

namespace TicketBookingSystem.CabinService.Service;

public interface ICabinTypeService
{
    Task<IEnumerable<CabinTypeDto>> GetAllCabinTypesAsync();
    Task<CabinTypeDto> CreateCabinTypeAsync(CabinTypeDto cabinTypeDto);
    Task<bool> DeleteCabinTypeAsync(int id);
}
