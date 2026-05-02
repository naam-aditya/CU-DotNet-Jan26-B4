using TicketBookingSystem.CabinService.Dtos;
using TicketBookingSystem.CabinService.Models;
using TicketBookingSystem.CabinService.Repository;
using TicketBookingSystem.CabinService.Exceptions;

namespace TicketBookingSystem.CabinService.Service;

public class CabinService : ICabinService
{
    private readonly ICabinRepository _repository;

    public CabinService(ICabinRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CabinDto>> GetCabinsByShipIdAsync(int shipId)
    {
        var cabins = await _repository.GetCabinsByShipIdAsync(shipId);
        return cabins.Select(MapToDto);
    }

    public async Task<CabinDto> GetCabinByIdAsync(int id)
    {
        var cabin = await _repository.GetCabinByIdAsync(id);
        if (cabin == null)
            throw new NotFoundException($"Cabin with ID {id} was not found.");
            
        return MapToDto(cabin);
    }

    public async Task<CabinDto> UpdateCabinAvailabilityAsync(int id, bool isAvailable)
    {
        var cabin = await _repository.GetCabinByIdAsync(id);
        if (cabin == null)
            throw new NotFoundException($"Cabin with ID {id} was not found.");

        cabin.IsAvailable = isAvailable;
        await _repository.UpdateCabinAsync(cabin);
        await _repository.SaveChangesAsync();
        
        return MapToDto(cabin);
    }

    private CabinDto MapToDto(Cabin cabin) => new CabinDto
    {
        Id = cabin.Id,
        ShipId = cabin.ShipId,
        CabinTypeId = cabin.CabinTypeId,
        CabinNumber = cabin.CabinNumber,
        DeckNumber = cabin.DeckNumber,
        IsAvailable = cabin.IsAvailable,
        CabinTypeName = cabin.CabinType?.Name
    };
}
