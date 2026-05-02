namespace TicketBookingSystem.CabinService.Models;

public class Amenity
{
    public int Id { get; set; }
    public int CabinTypeId { get; set; }
    public string Name { get; set; } = null!;
    public string? IconUrl { get; set; }

    public CabinType CabinType { get; set; } = null!;
}
