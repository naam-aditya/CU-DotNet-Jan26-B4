using System.ComponentModel.DataAnnotations;

namespace TicketBookingSystem.Mvc.ViewModels;

public class TripSearchViewModel
{
    [Display(Name = "Destination")]
    public string? Destination { get; set; }

    [Display(Name = "Departure Port")]
    public string? DeparturePort { get; set; }

    [DataType(DataType.Date), Display(Name = "Travel Month")]
    public DateTime? TravelMonth { get; set; }

    [Display(Name = "Nights")]
    public int? Nights { get; set; }
}
