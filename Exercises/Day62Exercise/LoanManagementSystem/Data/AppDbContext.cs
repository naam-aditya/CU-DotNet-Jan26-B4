using Microsoft.EntityFrameworkCore;
using LoanManagementSystem.Models;

namespace LoanManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<Loan> Loan { get; set; } = default!;
    }
}
