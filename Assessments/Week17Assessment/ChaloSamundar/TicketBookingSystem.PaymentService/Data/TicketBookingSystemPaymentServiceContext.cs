using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.PaymentService.Models;

namespace TicketBookingSystem.PaymentService.Data
{
    public class TicketBookingSystemPaymentServiceContext : DbContext
    {
        public TicketBookingSystemPaymentServiceContext (DbContextOptions<TicketBookingSystemPaymentServiceContext> options)
            : base(options)
        {
        }

        public DbSet<TicketBookingSystem.PaymentService.Models.Payment> Payment { get; set; } = default!;
    }
}
