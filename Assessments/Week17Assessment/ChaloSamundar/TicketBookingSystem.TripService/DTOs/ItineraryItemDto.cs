namespace TicketBookingSystem.TripService.DTOs
{
    public class ItineraryItemDto
    {
        public int DayNumber { get; set; }
        public string Location { get; set; } = default!;
        public DateTime Date { get; set; }
        public bool IsAtSea { get; set; }
    }
}
