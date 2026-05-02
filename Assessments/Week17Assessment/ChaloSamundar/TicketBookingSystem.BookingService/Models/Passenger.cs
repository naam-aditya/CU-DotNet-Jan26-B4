namespace TicketBookingSystem.BookingService.Models
{
    public class Passenger
    {
        public int Id { get; set; }
        public string BookingReference { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string Gender { get; set; } = null!;
    }
}
