namespace TicketBookingSystem.PaymentService.DTOs
{
    public class PaymentDTO
    {
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public string? PaymentMethod { get; set; }
        public string? CardHolderName { get; set; }
        public string? CardNumber { get; set; }
        public string? Expiry { get; set; }
        public string? CVV { get; set; }
    }
    public class CreateOrderDTO
    {
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "INR";
    }

    // New DTO for payment verification
    public class VerifyPaymentDTO
    {
        public int BookingId { get; set; }
        public string RazorpayOrderId { get; set; } = string.Empty;
        public string RazorpayPaymentId { get; set; } = string.Empty;
        public string RazorpaySignature { get; set; } = string.Empty;
    }
}
