using TicketBookingSystem.BookingService.Models;

namespace TicketBookingSystem.BookingService.Repository;

public interface IPassengerRepository
{
    Task AddPassengerAsync(Passenger passenger);
    Task AddPassengersAsync(List<Passenger> passengers);
    Task<List<Passenger>> GetPassengersByBookingReferenceAsync(string bookingReference);
}
