using TicketBookingSystem.PaymentService.DTOs;
using TicketBookingSystem.PaymentService.Models;

namespace TicketBookingSystem.PaymentService.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetAllPayments();
        Task<Payment> GetPaymentById(int id);
        Task<object> ProcessPayment(PaymentDTO dto);
        Task<string> ConfirmUPIPayment(int bookingId);
        Task<bool> DeletePayment(int id);
        // Razorpay
        Task<object> CreateRazorpayOrder(CreateOrderDTO dto);
        Task<object> VerifyAndSavePayment(VerifyPaymentDTO dto);
    }
}
