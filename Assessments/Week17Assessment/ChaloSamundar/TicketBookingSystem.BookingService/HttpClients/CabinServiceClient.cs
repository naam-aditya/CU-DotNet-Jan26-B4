using System.Net.Http.Json;

namespace TicketBookingSystem.BookingService.HttpClients;

public class CabinServiceClient : ICabinServiceClient
{
    private readonly HttpClient _httpClient;

    public CabinServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> IsCabinAvailableAsync(int cabinId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/cabins/{cabinId}/availability");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<bool>();
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateCabinAvailabilityAsync(int cabinId, bool isAvailable)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/cabins/{cabinId}/availability", isAvailable);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
