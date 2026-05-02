namespace TicketBookingSystem.CabinService.Dtos;

public class CabinImageDto
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = null!;
    public int DisplayOrder { get; set; }
}
