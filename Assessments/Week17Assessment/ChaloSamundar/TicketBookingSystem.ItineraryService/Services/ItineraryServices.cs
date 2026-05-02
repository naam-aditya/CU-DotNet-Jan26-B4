using TicketBookingSystem.ItineraryService.Models;
using TicketBookingSystem.ItineraryService.Repositories;

namespace TicketBookingSystem.ItineraryService.Services
{
    public class ItineraryServices : IItineraryService
    {
        private readonly IItineraryRepository _repo;

        public ItineraryServices(IItineraryRepository repo)
        {
            _repo = repo;
        }

        public Task<List<ItineraryItems>> GetByTripIdAsync(int tripId)
            => _repo.GetByTripIdAsync(tripId);

        public Task AddAsync(ItineraryItems item)
            => _repo.AddAsync(item);
        public async Task DeleteAsync(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item != null)
                await _repo.DeleteAsync(item);
        }
    }
}
