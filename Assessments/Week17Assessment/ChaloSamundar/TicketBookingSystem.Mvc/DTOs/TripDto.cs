namespace TicketBookingSystem.Mvc.DTOs;

public class TripDto
{
    public int Id { get; set; }
    public string? Heading { get; set; }
    public string? TripType { get; set; }
    public int Nights { get; set; }
    public int Price { get; set; }
}
