namespace TicketBookingSystem.ItineraryService.Models
{
    public class ItineraryItems
    {
        public int Id { get; set; }

        public int TripId { get; set; } // 🔥 link to Trip

        public int DayNumber { get; set; }

        public string Location { get; set; } = default!;

        public DateTime Date { get; set; }

        public bool IsAtSea { get; set; }
    }
}
