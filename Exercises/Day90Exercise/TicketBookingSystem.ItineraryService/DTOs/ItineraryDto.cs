namespace TicketBookingSystem.ItineraryService.DTOs
{
    public class CreateItineraryItemDto
    {
        public int TripId { get; set; }
        public int DayNumber { get; set; }
        public string Location { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public bool IsAtSea { get; set; }
    }

    public class UpdateItineraryItemDto
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public int DayNumber { get; set; }
        public string Location { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public bool IsAtSea { get; set; }
    }

    public class ItineraryItemDto
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public int DayNumber { get; set; }
        public string Location { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public bool IsAtSea { get; set; }
        public string FormattedDate { get; set; } = string.Empty; // Calculated field
        public string Status { get; set; } = string.Empty; // Calculated field
        public int DaysPassed { get; set; } // Calculated field
    }

    public class ItineraryTripSummaryDto
    {
        public int TripId { get; set; }
        public int TotalDays { get; set; }
        public int AtSeaDays { get; set; }
        public int PortDays { get; set; }
        public List<string> Locations { get; set; } = new List<string>();
        public List<ItineraryItemDto> Items { get; set; } = new List<ItineraryItemDto>();
    }
}
