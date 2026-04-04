using Microsoft.EntityFrameworkCore;
using Vagabond.TravelDestinationService.Models;

namespace Vagabond.TravelDestinationService.Data
{
    public class DestinationDbContext : DbContext
    {
        public DestinationDbContext (DbContextOptions<DestinationDbContext> options)
            : base(options) {}

        public DbSet<Destination> Destination { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Destination>(
                entity =>
                {
                    entity.ToTable("Destinations");

                    entity.Property(e => e.CityName)
                        .IsRequired();
                    
                    entity.Property(e => e.Country)
                        .IsRequired();

                    entity.Property(e => e.Description)
                        .HasMaxLength(200);
                    
                    entity.Property(e => e.Rating)
                        .HasDefaultValue(3);
                }
            );
        }
    }
}
