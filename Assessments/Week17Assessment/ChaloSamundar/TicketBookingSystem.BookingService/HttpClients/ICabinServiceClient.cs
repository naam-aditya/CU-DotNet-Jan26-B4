namespace TicketBookingSystem.BookingService.HttpClients;

public interface ICabinServiceClient
{
    Task<bool> IsCabinAvailableAsync(int cabinId);
    Task<bool> UpdateCabinAvailabilityAsync(int cabinId, bool isAvailable);
}
