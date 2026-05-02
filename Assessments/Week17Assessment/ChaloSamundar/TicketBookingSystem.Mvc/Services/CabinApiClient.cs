using System.Net.Http.Headers;
using System.Text.Json;
using TicketBookingSystem.Mvc.ViewModels;

namespace TicketBookingSystem.Mvc.Services;

public interface ICabinWebService
{
    Task<List<CabinTypeViewModel>> GetCabinTypesAsync();
    
}

public class CabinWebService : ICabinWebService
{
    private readonly HttpClient _httpClient;

    

    public CabinWebService(HttpClient httpClient, IHttpClientFactory factory, IHttpContextAccessor http)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CabinTypeViewModel>> GetCabinTypesAsync()
    {
        var response = await _httpClient.GetAsync("api/CabinTypes");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<CabinTypeViewModel>>() ?? new();
        }
        return new();
    }
}
