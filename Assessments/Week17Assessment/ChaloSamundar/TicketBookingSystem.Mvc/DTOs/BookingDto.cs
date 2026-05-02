using TicketBookingSystem.Mvc.ViewModels;

namespace TicketBookingSystem.Mvc.DTOs;

public class BookingDto
{
    public string BookingReference { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public int Status { get; set; } 

    public string SourcePort { get; set; } = string.Empty;
    public string SourcePortCode { get; set; } = string.Empty;
    public string DestinationPort { get; set; } = string.Empty;
    public string DestinationPortCode { get; set; } = string.Empty;
    public string JourneyName { get; set; } = string.Empty;
    public string CabinType { get; set; } = string.Empty;

    public DateTime Embarkation { get; set; }
    public DateTime Disembarkation { get; set; }
    public DateTime BookingDate { get; set; }

    public int Nights { get; set; }
    public int NumberOfPassengers { get; set; }  // ← fallback when Passengers list is empty

    public decimal BaseFare { get; set; }
    public decimal ConvenienceFee { get; set; }
    public decimal TotalFare { get; set; }

    public List<PassengerViewModel> Passengers { get; set; } = [];
}
