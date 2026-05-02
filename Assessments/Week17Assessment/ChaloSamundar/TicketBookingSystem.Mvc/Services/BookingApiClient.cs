using System.Net.Http.Headers;
using System.Text.Json;
using TicketBookingSystem.Mvc.DTOs;

namespace TicketBookingSystem.Mvc.Services;

public class BookingApiClient : IBookingApiClient
{
    private readonly HttpClient _client;
    private readonly IHttpContextAccessor _http;
    private const string BookingEndpoint = "api/booking";

    public BookingApiClient(HttpClient client, IHttpContextAccessor http)
    {
        _client = client;
        _http = http;
    }

    /// <summary>
    /// Pulls the JWT from the user's session and attaches it as a Bearer token.
    /// BookingService is decorated with [Authorize] — without this, every call returns 401.
    /// </summary>
    private void AttachAuthHeader()
    {
        var token = _http.HttpContext?.Session.GetString("JWT");
        _client.DefaultRequestHeaders.Authorization =
            !string.IsNullOrWhiteSpace(token)
                ? new AuthenticationHeaderValue("Bearer", token)
                : null;
    }

    public async Task<BookingDto?> CreateBookingAsync(BookingDto booking)
    {
        AttachAuthHeader();

        var response = await _client.PostAsJsonAsync(BookingEndpoint, booking);
        //if (!response.IsSuccessStatusCode)
        //{
        //    // Surface the API's own error body in the dev console so we can debug fast.
        //    var body = await response.Content.ReadAsStringAsync();
        //    Console.WriteLine($"BookingService rejected ({(int)response.StatusCode} {response.StatusCode}): {body}");
        //    return null;
        //}
        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"BookingService rejected ({(int)response.StatusCode}): {body}");
            throw new Exception($"BookingService error ({(int)response.StatusCode}): {body}");
        }

        var stream = await response.Content.ReadAsStreamAsync();
        var json = await JsonDocument.ParseAsync(stream);
        var created = json.RootElement.GetProperty("data").GetRawText();

        return JsonSerializer.Deserialize<BookingDto>(created ?? string.Empty,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<BookingDto?> GetBookingByReferenceAsync(string bookingReference)
    {
        AttachAuthHeader();

        try
        {
            return await _client.GetFromJsonAsync<BookingDto>($"{BookingEndpoint}/{bookingReference}");
        }
        catch { return null; }
    }
    //new 
    public async Task<List<BookingDto>> GetBookingsByUserIdAsync(string userId)
    {
        //AttachAuthHeader();
        //try
        //{
        //    var response = await _client.GetAsync($"{BookingEndpoint}/user/{userId}");
        //    if (!response.IsSuccessStatusCode) return [];

        //    var stream = await response.Content.ReadAsStreamAsync();
        //    var json = await JsonDocument.ParseAsync(stream);

        //    // Handle both { data: [...] } and direct array responses
        //    JsonElement dataElement;
        //    if (json.RootElement.TryGetProperty("data", out var data))
        //        dataElement = data;
        //    else
        //        dataElement = json.RootElement;

        //    return JsonSerializer.Deserialize<List<BookingDto>>(
        //        dataElement.GetRawText(),
        //        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        //    ) ?? [];
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"GetBookingsByUserIdAsync failed: {ex.Message}");
        //    return [];
        //}
        AttachAuthHeader();
        try
        {
            var response = await _client.GetAsync($"{BookingEndpoint}/user/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                var err = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"GetBookingsByUserId failed ({(int)response.StatusCode}): {err}");
                return [];
            }

            var stream = await response.Content.ReadAsStreamAsync();
            var json = await JsonDocument.ParseAsync(stream);

            // API returns { success, data: [...], message, ... }
            var dataRaw = json.RootElement.TryGetProperty("data", out var dataProp)
                ? dataProp.GetRawText()
                : json.RootElement.GetRawText();

            return JsonSerializer.Deserialize<List<BookingDto>>(dataRaw, 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"GetBookingsByUserIdAsync exception: {ex.Message}");
            return [];
        }
    }
}
