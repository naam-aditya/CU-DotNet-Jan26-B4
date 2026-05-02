namespace TicketBookingSystem.Mvc.ViewModels;

public class ItineraryItemViewModel
{
    //public int DayNumber { get; set; }
    //public string Location { get; set; } = "";
    //public DateTime Date { get; set; }
    //public bool IsAtSea { get; set; }
    public int Id { get; set; }
    public int TripId { get; set; }
    public int DayNumber { get; set; }
    public string Location { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public bool IsAtSea { get; set; }
}
