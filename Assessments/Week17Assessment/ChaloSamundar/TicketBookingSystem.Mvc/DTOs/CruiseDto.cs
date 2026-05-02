namespace TicketBookingSystem.Mvc.DTOs;

public class CruiseDto
{
    public int CruiseId { get; set; }
    public string? Name { get; set; }
    public string? Destination { get; set; }
    public DateTime DepartureDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
}
