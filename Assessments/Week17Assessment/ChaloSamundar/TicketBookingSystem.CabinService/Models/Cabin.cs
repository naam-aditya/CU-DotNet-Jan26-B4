namespace TicketBookingSystem.CabinService.Models;

//public enum CabinStatus { Available, Reserved, Booked }
public class Cabin
{
    public int Id { get; set; }
    //public int TripId { get; set; }
    public int ShipId { get; set; }
    public int CabinTypeId { get; set; }
    public string CabinNumber { get; set; } = null!;
    //public CabinStatus Status { get; set; } = CabinStatus.Available;
    //public string? BookingReference { get; set; }           // set when Reserved/Booked
    //public DateTime? ReservedAt { get; set; }
    public int DeckNumber { get; set; }
    public bool IsAvailable { get; set; }

    public CabinType CabinType { get; set; } = null!;
}
