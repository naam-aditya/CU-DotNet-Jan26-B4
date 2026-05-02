using System.ComponentModel.DataAnnotations;

namespace TicketBookingSystem.Mvc.ViewModels;

public class BookingViewModel
{
    public string BookingReference { get; set; } = string.Empty;
    public string SourcePort { get; set; } = string.Empty;
    public string DestinationPort { get; set; } = string.Empty;
    public string SourcePortCode { get; set; } = string.Empty;
    public string DestinationPortCode { get; set; } = string.Empty;
    public string JourneyName { get; set; } = string.Empty;
    public string CabinType { get; set; } = string.Empty;

    public DateTime Embarkation { get; set; }
    public DateTime Disembarkation { get; set; }

    public int Nights { get; set; }

    // Base cruise price (from the Trip). Stored separately so TotalFare can be recomputed.
    public decimal CruisePrice { get; set; }

    // Per-person cost for the chosen cabin type.
    public decimal CabinPricePerPerson { get; set; }

    // TotalFare = CruisePrice + (Passengers.Count * CabinPricePerPerson)
    public decimal TotalFare { get; set; }

    public List<PassengerViewModel> Passengers { get; set; } = [];
}
