using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.PaymentService.Data;
using TicketBookingSystem.PaymentService.Models;

namespace TicketBookingSystem.PaymentService.Repositories
{
    public class PaymentRepository: IPaymentRepository
    {
        private readonly TicketBookingSystemPaymentServiceContext _context;

        public PaymentRepository(TicketBookingSystemPaymentServiceContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await _context.Payment.ToListAsync();
        }
        public async Task<Payment> GetByIdAsync(int id)
        {
            return await _context.Payment.FindAsync(id);
        }
        public async Task<Payment> GetByBookingIdAsync(int bookingId)
        {
            return await _context.Payment
                .FirstOrDefaultAsync(p => p.BookingId == bookingId);
        }
        public async Task AddAsync(Payment payment)
        {
            await _context.Payment.AddAsync(payment);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Payment payment)
        {
            _context.Payment.Update(payment);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Payment payment)
        {
            _context.Payment.Remove(payment);
            await _context.SaveChangesAsync();
        }
    }
}
