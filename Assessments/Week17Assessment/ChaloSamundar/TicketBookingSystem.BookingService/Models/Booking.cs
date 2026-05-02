namespace TicketBookingSystem.BookingService.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string BookingReference { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public int CruiseId { get; set; }
        public int CabinId { get; set; }
        public int ItineraryId { get; set; }

        public string SourcePort { get; set; } = null!;
        public string SourcePortCode{ get; set; } = null!;

        public string DestinationPort { get; set; } = null!;
        public string DestinationPortCode { get; set; } = null!;

        public string JourneyName { get; set; } = null!;
        public string CabinType { get; set; } = null!;

        public DateTime BookingDate { get; set; }
        public DateTime Embarkation { get; set; }
        public DateTime Disembarkation { get; set; }

        public int NumberOfPassengers { get; set; }
        public int Nights { get; set; }

        public decimal BaseFare { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal ServiceFee { get; set; }
        public decimal ConvenienceFee { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalFare { get; set; }

        public string Status { get; set; } = BookingStatus.Initiated.ToString();
        public string PaymentStatus { get; set; } = BookingPaymentStatus.Initiated.ToString();

        public DateTime? CancelledAt { get; set; }
        public string? CancellationReason { get; set; }
        public decimal? RefundAmount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public enum BookingStatus
    {
        Confirmed,
        Cancelled,
        Initiated
    }

    public enum BookingPaymentStatus
    {
        Initiated,
        Successful,
        Cancelled,
        Failed,
        Refunded
    }
}
