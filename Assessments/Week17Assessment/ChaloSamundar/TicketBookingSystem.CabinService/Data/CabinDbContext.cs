using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.CabinService.Models;

namespace TicketBookingSystem.CabinService.Data;

public class CabinDbContext : DbContext
{
    public CabinDbContext(DbContextOptions<CabinDbContext> options) : base(options)
    {
    }

    public DbSet<CabinType> CabinTypes { get; set; }
    public DbSet<Cabin> Cabins { get; set; }
    public DbSet<Amenity> Amenities { get; set; }
    public DbSet<CabinImage> CabinImages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships
        modelBuilder.Entity<CabinType>()
            .HasMany(ct => ct.Amenities)
            .WithOne(a => a.CabinType)
            .HasForeignKey(a => a.CabinTypeId);

        modelBuilder.Entity<CabinType>()
            .HasMany(ct => ct.Images)
            .WithOne(ci => ci.CabinType)
            .HasForeignKey(ci => ci.CabinTypeId);

        modelBuilder.Entity<CabinType>()
            .HasMany(ct => ct.Cabins)
            .WithOne(c => c.CabinType)
            .HasForeignKey(c => c.CabinTypeId);
            
        // Pricing precision
        modelBuilder.Entity<CabinType>()
            .Property(ct => ct.PricePerPerson)
            .HasPrecision(18, 2);
    }
}
