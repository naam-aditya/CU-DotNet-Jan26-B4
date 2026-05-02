using System;
using TicketBookingSystem.Mvc.ViewModels;

namespace TicketBookingSystem.Mvc.Services;

public class TripApiClient : ITripApiClient
{
    private readonly HttpClient _client;

    public TripApiClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<IReadOnlyList<TripViewModel>> SearchAsync(TripSearchViewModel criteria)
    {
        var trips = await _client.GetFromJsonAsync<IEnumerable<TripViewModel>>("api/trips") ?? [];

        if (!string.IsNullOrWhiteSpace(criteria.Destination))
            trips = trips.Where(t =>
                    (t.Heading != null && t.Heading.Contains(criteria.Destination, StringComparison.OrdinalIgnoreCase)) ||
                    t.Ports.Any(p => p.Contains(criteria.Destination, StringComparison.OrdinalIgnoreCase)));

        if (!string.IsNullOrWhiteSpace(criteria.DeparturePort))
            trips = trips.Where(t => t.Ports.Length > 0 &&
                    t.Ports[0].Equals(criteria.DeparturePort, StringComparison.OrdinalIgnoreCase));

        if (criteria.TravelMonth.HasValue)
            trips = trips.Where(t =>
                    t.StartDate.Year == criteria.TravelMonth.Value.Year &&
                    t.StartDate.Month == criteria.TravelMonth.Value.Month);

        if (criteria.Nights.HasValue)
            trips = trips.Where(t => t.Nights == criteria.Nights.Value);

        return trips.OrderBy(t => t.StartDate).ToList();
    }

    public async Task<TripViewModel?> GetByIdAsync(int id)
    {
        // Use SendAsync so we can inspect the status code instead of letting
        // GetFromJsonAsync throw on non-2xx (e.g. when the trip id doesn't exist).
        using var resp = await _client.GetAsync($"api/trips/{id}");

        if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;            // missing trip → caller decides (booking page falls back to defaults)

        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<TripViewModel>();
    }
}
