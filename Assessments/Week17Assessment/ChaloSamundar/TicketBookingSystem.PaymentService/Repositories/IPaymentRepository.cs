using TicketBookingSystem.PaymentService.Models;

namespace TicketBookingSystem.PaymentService.Repositories
{
    public interface IPaymentRepository
    {
        Task AddAsync(Payment payment);
        Task<Payment> GetByBookingIdAsync(int bookingId);
        Task UpdateAsync(Payment payment);
        Task<IEnumerable<Payment>> GetAllAsync();
        Task<Payment> GetByIdAsync(int id);
        Task DeleteAsync(Payment payment);
    }
}
