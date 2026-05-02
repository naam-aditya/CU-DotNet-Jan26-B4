using TicketBookingSystem.BookingService.DTOs;
using TicketBookingSystem.BookingService.Repository;

namespace TicketBookingSystem.BookingService.Service
{
    public class CabinAvailabilityService: ICabinAvailabilityService
    {
        private readonly ICabinAvailabilityRepository _repo;
        public CabinAvailabilityService(ICabinAvailabilityRepository repo) => _repo = repo;

        public async Task<bool> IsAvailableAsync(int tripId, int cabinTypeId)
        {
            var cabin = await _repo.GetAsync(tripId, cabinTypeId);
            return cabin != null && cabin.AvailableCabins > 0;
        }

        public async Task<int> GetAvailableCountAsync(int tripId, int cabinTypeId)
        {
            var cabin = await _repo.GetAsync(tripId, cabinTypeId);
            return cabin?.AvailableCabins ?? 0;
        }

        public async Task<bool> DecrementAsync(int tripId, int cabinTypeId)
        {
            var cabin = await _repo.GetAsync(tripId, cabinTypeId);
            if (cabin == null || cabin.AvailableCabins <= 0) return false;
            cabin.BookedCabins++;
            await _repo.UpdateAsync(cabin);
            return true;
        }

        public async Task<bool> IncrementAsync(int tripId, int cabinTypeId)
        {
            var cabin = await _repo.GetAsync(tripId, cabinTypeId);
            if (cabin == null) return false;
            if (cabin.BookedCabins > 0) cabin.BookedCabins--;
            await _repo.UpdateAsync(cabin);
            return true;
        }

        public async Task<List<CabinAvailabilityDto>> GetAllForTripAsync(int tripId)
        {
            var cabins = await _repo.GetByTripAsync(tripId);
            return cabins.Select(c => new CabinAvailabilityDto
            {
                CabinTypeId = c.CabinTypeId,
                CabinTypeName = c.CabinTypeName,
                TotalCabins = c.TotalCabins,
                BookedCabins = c.BookedCabins,
                AvailableCabins = c.AvailableCabins,
                IsAvailable = c.AvailableCabins > 0
            }).ToList();
        }
    }
}
