namespace TicketBookingSystem.Mvc.ViewModels;

public class CruiseViewModel
{
    public int CruiseId { get; set; }

    public string Name { get; set; } = null!;

    public string Destination { get; set; } = null!;

    public DateTime DepartureDate { get; set; }

    public DateTime ReturnDate { get; set; }

    public decimal Price { get; set; }

    public string Description { get; set; } = null!;

    public ICollection<BookingViewModel> Bookings { get; set; } = [];
}
