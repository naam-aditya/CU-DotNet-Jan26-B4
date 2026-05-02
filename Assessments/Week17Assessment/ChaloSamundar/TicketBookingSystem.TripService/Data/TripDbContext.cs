using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TicketBookingSystem.TripService.Data
{
    public class TripDbContext : DbContext
    {
        public TripDbContext (DbContextOptions<TripDbContext> options)
            : base(options) {}

        public DbSet<TicketBookingSystem.TripService.Models.Trip> Trip { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var splitConverter = new ValueConverter<string[], string>(
                v => string.Join(';', v ?? System.Array.Empty<string>()),
                v => string.IsNullOrEmpty(v) ? System.Array.Empty<string>() : v.Split(';', System.StringSplitOptions.None));

            var arrayComparer = new Microsoft.EntityFrameworkCore.ChangeTracking.ValueComparer<string[]>(
                (a, b) => (a ?? System.Array.Empty<string>()).SequenceEqual(b ?? System.Array.Empty<string>()),
                v => v == null ? 0 : v.Aggregate(0, (h, s) => System.HashCode.Combine(h, s == null ? 0 : s.GetHashCode())),
                v => v == null ? System.Array.Empty<string>() : v.ToArray());

            modelBuilder.Entity<TicketBookingSystem.TripService.Models.Trip>()
                .Property(t => t.Ports)
                .HasConversion(splitConverter)
                .Metadata.SetValueComparer(arrayComparer);
        }
    }
}
