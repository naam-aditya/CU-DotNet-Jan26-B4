using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.ItineraryService.Models;

namespace TicketBookingSystem.ItineraryService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ItineraryItems> ItineraryItems => Set<ItineraryItems>();
    }
}
