using TicketBookingSystem.TripService.DTOs;
using TicketBookingSystem.TripService.Models;

namespace TicketBookingSystem.TripService.Services
{    
    public interface ITripService
    {
        Task<IEnumerable<Trip>> GetAllTripsAsync();
        Task<TripWithItineraryDto> GetTripByIdAsync(int id);
        Task<Trip> CreateTripAsync(CreateTripDto dto);
        Task UpdateTripAsync(int id, Trip trip);
        Task DeleteTripAsync(int id);
    }
}