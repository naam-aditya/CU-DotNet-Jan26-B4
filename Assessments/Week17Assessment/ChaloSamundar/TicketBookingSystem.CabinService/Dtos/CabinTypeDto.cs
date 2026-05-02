namespace TicketBookingSystem.CabinService.Dtos;

public class CabinTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int MaxOccupancy { get; set; }
    public decimal PricePerPerson { get; set; }
    public int BaseOccupancy { get; set; }
    public bool IsActive { get; set; }

    public List<AmenityDto> Amenities { get; set; } = new();
    public List<CabinImageDto> Images { get; set; } = new();
}
