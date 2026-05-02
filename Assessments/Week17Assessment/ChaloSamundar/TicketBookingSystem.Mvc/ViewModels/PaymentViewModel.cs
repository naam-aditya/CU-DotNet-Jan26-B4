namespace TicketBookingSystem.Mvc.ViewModels;

public class PaymentViewModel
{
    public int BookingId { get; set; }
    public decimal Amount { get; set; }
    public string? PaymentMethod { get; set; }
    public string? CardNumber { get; set; }
    public string? CardHolderName { get; set; }
    public string? Expiry { get; set; }
    public string? CVV { get; set; }
    public string? UpiId { get; set; }
    public string? SelectedBank { get; set; }
    public string? QRCodeBase64 { get; set; }
    public string? ErrorMessage { get; set; }
    public string? RazorpayOrderId { get; set; }
    public int RazorpayAmount { get; set; }
    public string? RazorpayKeyId { get; set; }
}

public class VerifyPaymentViewModel
{
    public int BookingId { get; set; }
    public decimal Amount { get; set; }
    public string RazorpayOrderId { get; set; } = string.Empty;
    public string RazorpayPaymentId { get; set; } = string.Empty;
    public string RazorpaySignature { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
}

public class PaymentSuccessViewModel
{
    public int PaymentId { get; set; }
    public int BookingId { get; set; }
    public decimal Amount { get; set; }
    public string? Status { get; set; }
    public string? PaymentMethod { get; set; }
    public string? RazorpayPaymentId { get; set; }
}
