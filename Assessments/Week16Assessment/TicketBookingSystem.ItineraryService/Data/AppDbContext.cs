using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.ItineraryService.Models;

namespace TicketBookingSystem.ItineraryService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ItineraryItems> ItineraryItems => Set<ItineraryItems>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure ItineraryItems entity with Fluent API
            modelBuilder.Entity<ItineraryItems>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Id");

                entity.Property(e => e.TripId)
                    .IsRequired()
                    .HasColumnName("TripId");

                entity.Property(e => e.DayNumber)
                    .IsRequired()
                    .HasColumnName("DayNumber");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("Location");

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasColumnName("Date");

                entity.Property(e => e.IsAtSea)
                    .IsRequired()
                    .HasColumnName("IsAtSea")
                    .HasDefaultValue(false);

                // Indexes for performance
                entity.HasIndex(e => e.TripId)
                    .HasDatabaseName("IX_ItineraryItems_TripId");

                entity.HasIndex(e => new { e.TripId, e.DayNumber })
                    .HasDatabaseName("IX_ItineraryItems_TripId_DayNumber")
                    .IsUnique();

                // Table configuration
                entity.ToTable("ItineraryItems");
            });
        }
    }
}
