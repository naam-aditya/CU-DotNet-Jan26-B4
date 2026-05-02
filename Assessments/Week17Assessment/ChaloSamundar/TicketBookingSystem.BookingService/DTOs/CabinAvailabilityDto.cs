namespace TicketBookingSystem.BookingService.DTOs
{
    public class CabinAvailabilityDto
    {
        public int CabinTypeId { get; set; }
        public string CabinTypeName { get; set; } = string.Empty;
        public int TotalCabins { get; set; }
        public int BookedCabins { get; set; }
        public int AvailableCabins { get; set; }
        public bool IsAvailable { get; set; }
    }
}
