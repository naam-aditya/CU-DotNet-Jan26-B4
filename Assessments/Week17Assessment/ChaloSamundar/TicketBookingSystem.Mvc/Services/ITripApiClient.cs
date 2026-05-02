using TicketBookingSystem.Mvc.ViewModels;

namespace TicketBookingSystem.Mvc.Services;

public interface ITripApiClient
{
    Task<IReadOnlyList<TripViewModel>> SearchAsync(TripSearchViewModel criteria);
    Task<TripViewModel?> GetByIdAsync(int id);
}
