namespace TicketBookingSystem.CabinService.Models;

public class CabinImage
{
    public int Id { get; set; }
    public int CabinTypeId { get; set; }
    public string ImageUrl { get; set; } = null!;
    public int DisplayOrder { get; set; }

    public CabinType CabinType { get; set; } = null!;
}
