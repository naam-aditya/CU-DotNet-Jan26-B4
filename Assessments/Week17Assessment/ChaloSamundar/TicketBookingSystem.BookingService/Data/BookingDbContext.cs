using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.BookingService.Models;

namespace TicketBookingSystem.BookingService.Data
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options)
        : base(options) { }

        public DbSet<Booking> Bookings { get; set; } = default!;
        public DbSet<Passenger> Passengers { get; set; } = default!;
        //public object CabinAvailability { get; internal set; }
        public DbSet<CabinAvailability> CabinAvailabilities { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasIndex(x => x.BookingReference)
                    .IsUnique();
            });
            // ── Seed cabin availability for each trip + cabin type ──
            // Adjust TripId and CabinTypeId to match your actual data
            // TripIds: 1, 2, 3 ... (from your TripService)
            // CabinTypeIds: 1=Interior, 2=Ocean View, 3=Balcony, 4=Suite (from your CabinService)
            //var seed = new List<CabinAvailability>();
            //int id = 1;
            //for (int tripId = 1; tripId <= 10; tripId++)
            //{
            //    seed.Add(new CabinAvailability { Id = id++, TripId = tripId, CabinTypeId = 1, CabinTypeName = "Interior", TotalCabins = 20, BookedCabins = 0 });
            //    seed.Add(new CabinAvailability { Id = id++, TripId = tripId, CabinTypeId = 2, CabinTypeName = "Ocean View", TotalCabins = 15, BookedCabins = 0 });
            //    seed.Add(new CabinAvailability { Id = id++, TripId = tripId, CabinTypeId = 3, CabinTypeName = "Balcony", TotalCabins = 10, BookedCabins = 0 });
            //    seed.Add(new CabinAvailability { Id = id++, TripId = tripId, CabinTypeId = 4, CabinTypeName = "Suite", TotalCabins = 5, BookedCabins = 0 });
            //}
            //modelBuilder.Entity<CabinAvailability>().HasData(seed);
        }
    }
}
