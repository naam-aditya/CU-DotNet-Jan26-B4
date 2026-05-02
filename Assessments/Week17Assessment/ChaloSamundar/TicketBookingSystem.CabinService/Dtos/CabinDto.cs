namespace TicketBookingSystem.CabinService.Dtos;

public class CabinDto
{
    public int Id { get; set; }
    public int ShipId { get; set; }
    public int CabinTypeId { get; set; }
    public string CabinNumber { get; set; } = null!;
    public int DeckNumber { get; set; }
    public bool IsAvailable { get; set; }
    
    public string? CabinTypeName { get; set; }
}
