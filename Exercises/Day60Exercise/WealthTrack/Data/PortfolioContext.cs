using Microsoft.EntityFrameworkCore;
using WealthTrack.Models;

    public class PortfolioContext : DbContext
    {
        public PortfolioContext (DbContextOptions<PortfolioContext> options)
            : base(options)
        { }

        public DbSet<Investment> Investment { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.Entity<Investment>()
            .Property(i => i.PurchasePrice)
            .HasPrecision(18, 4);
}
