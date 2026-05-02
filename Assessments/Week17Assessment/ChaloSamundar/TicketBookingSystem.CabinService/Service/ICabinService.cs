using TicketBookingSystem.CabinService.Dtos;

namespace TicketBookingSystem.CabinService.Service;

public interface ICabinService
{
    Task<IEnumerable<CabinDto>> GetCabinsByShipIdAsync(int shipId);
    Task<CabinDto> GetCabinByIdAsync(int id);
    Task<CabinDto> UpdateCabinAvailabilityAsync(int id, bool isAvailable);
}
