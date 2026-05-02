using TicketBookingSystem.TripService.Models;

namespace TicketBookingSystem.TripService.Repositories
{
    public interface ITripRepository
    {
        Task<IEnumerable<Trip>> GetAllAsync();
        Task<Trip?> GetByIdAsync(int id);
        Task AddAsync(Trip trip);
        Task UpdateAsync(Trip trip);
        Task DeleteAsync(Trip trip);
        Task<bool> ExistsAsync(int id);
    }
}