namespace TicketBookingSystem.TripService.DTOs
{
    public class TripWithItineraryDto
    {
        public int Id { get; set; }
        public string? Heading { get; set; }
        public string? ShipName { get; set; }
        public string? TripType { get; set; }
        public string[] Ports { get; set; } = [];
        public int Nights { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }

        public List<ItineraryItemDto> Itinerary { get; set; } = new();
    }
}
