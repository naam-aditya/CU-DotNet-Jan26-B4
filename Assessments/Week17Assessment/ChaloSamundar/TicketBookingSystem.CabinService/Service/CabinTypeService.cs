using TicketBookingSystem.CabinService.Dtos;
using TicketBookingSystem.CabinService.Models;
using TicketBookingSystem.CabinService.Repository;
using TicketBookingSystem.CabinService.Exceptions;

namespace TicketBookingSystem.CabinService.Service;

public class CabinTypeService : ICabinTypeService
{
    private readonly ICabinTypeRepository _repository;

    public CabinTypeService(ICabinTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CabinTypeDto>> GetAllCabinTypesAsync()
    {
        var types = await _repository.GetAllCabinTypesAsync();
        return types.Select(MapToDto);
    }

    public async Task<CabinTypeDto> CreateCabinTypeAsync(CabinTypeDto cabinTypeDto)
    {
        if (string.IsNullOrEmpty(cabinTypeDto.Name))
            throw new BadRequestException("Cabin type name is required.");

        var type = new CabinType
        {
            Name = cabinTypeDto.Name,
            Description = cabinTypeDto.Description,
            MaxOccupancy = cabinTypeDto.MaxOccupancy,
            PricePerPerson = cabinTypeDto.PricePerPerson,
            BaseOccupancy = cabinTypeDto.BaseOccupancy,
            IsActive = cabinTypeDto.IsActive
        };

        await _repository.AddCabinTypeAsync(type);
        await _repository.SaveChangesAsync();

        cabinTypeDto.Id = type.Id;
        return cabinTypeDto;
    }

    public async Task<bool> DeleteCabinTypeAsync(int id)
    {
        var type = await _repository.GetCabinTypeByIdAsync(id);
        if (type == null)
            throw new NotFoundException($"Cabin type with ID {id} was not found.");

        await _repository.DeleteCabinTypeAsync(id);
        await _repository.SaveChangesAsync();
        return true;
    }

    private CabinTypeDto MapToDto(CabinType type) => new CabinTypeDto
    {
        Id = type.Id,
        Name = type.Name,
        Description = type.Description,
        MaxOccupancy = type.MaxOccupancy,
        PricePerPerson = type.PricePerPerson,
        BaseOccupancy = type.BaseOccupancy,
        IsActive = type.IsActive,
        Amenities = type.Amenities.Select(a => new AmenityDto
        {
            Id = a.Id,
            Name = a.Name,
            IconUrl = a.IconUrl
        }).ToList(),
        Images = type.Images.Select(i => new CabinImageDto
        {
            Id = i.Id,
            ImageUrl = i.ImageUrl,
            DisplayOrder = i.DisplayOrder
        }).OrderBy(i => i.DisplayOrder).ToList()
    };
}
