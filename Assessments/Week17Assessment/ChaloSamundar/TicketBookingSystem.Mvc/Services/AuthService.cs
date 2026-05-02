using TicketBookingSystem.Mvc.DTOs;

namespace TicketBookingSystem.Mvc.Services;

public class AuthService
{
    private readonly HttpClient _httpclient;

    public AuthService(HttpClient httpClient)
    {
        _httpclient = httpClient;
    }

    public async Task<bool> RegisterAsync(RegisterDto dto)
    {
        var response = await _httpclient.PostAsJsonAsync("api/Auth/register", dto);
        return response.IsSuccessStatusCode;
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var response = await _httpclient.PostAsJsonAsync("api/Auth/login", dto);
        if (!response.IsSuccessStatusCode)
            return null!;
        var result = await response.Content.ReadFromJsonAsync<TokenResponse>(new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return result?.Token!;
    }

    public class TokenResponse
    {
        public string Token { get; set; } = null!;
    }
}
