namespace TicketBookingSystem.BookingService.Models
{
    public class CabinAvailability
    {
        public int Id { get; set; }
        public int TripId { get; set; }         // which cruise trip
        public int CabinTypeId { get; set; }    // which cabin type
        public string CabinTypeName { get; set; } = string.Empty;
        public int TotalCabins { get; set; }    // fixed total e.g. 20
        public int BookedCabins { get; set; }   // increases on booking
        public int AvailableCabins => TotalCabins - BookedCabins;
    }
}
