using System.Net.Http.Headers;
using System.Text.Json;
using TicketBookingSystem.Mvc.ViewModels;

namespace TicketBookingSystem.Mvc.Services
{
    public class ProfileApiClient
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _http;
        private const string BaseUrl = "https://localhost:7001/"; // ← your AuthService port

        private static readonly JsonSerializerOptions JsonOpts = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public ProfileApiClient(HttpClient client, IHttpContextAccessor http)
        {
            _client = client;
            _http = http;
        }

        private void AttachAuthHeader()
        {
            var token = _http.HttpContext?.Session.GetString("JWT");
            _client.DefaultRequestHeaders.Authorization =
                !string.IsNullOrWhiteSpace(token)
                    ? new AuthenticationHeaderValue("Bearer", token)
                    : null;
        }

        public async Task<UserProfileViewModel?> GetProfileAsync()
        {
            AttachAuthHeader();
            try
            {
                var res = await _client.GetAsync($"{BaseUrl}api/profile");
                if (!res.IsSuccessStatusCode) return null;

                var json = await res.Content.ReadAsStringAsync();
                var dto = JsonSerializer.Deserialize<ProfileDto>(json, JsonOpts);
                if (dto == null) return null;

                return new UserProfileViewModel
                {
                    FullName = dto.FullName ?? string.Empty,
                    DateOfBirth = dto.DateOfBirth,
                    Gender = dto.Gender ?? string.Empty,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    City = dto.City,
                    IsSaved = dto.FullName != null
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProfileApiClient] GetProfile failed: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> SaveProfileAsync(UserProfileViewModel model)
        {
            AttachAuthHeader();
            try
            {
                var payload = new ProfileDto
                {
                    FullName = model.FullName,
                    DateOfBirth = model.DateOfBirth,
                    Gender = model.Gender,
                    PhoneNumber = model.PhoneNumber,
                    City = model.City
                };
                var res = await _client.PutAsJsonAsync($"{BaseUrl}api/profile", payload);
                return res.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProfileApiClient] SaveProfile failed: {ex.Message}");
                return false;
            }
        }

        // Internal DTO matching AuthService response
        private class ProfileDto
        {
            public string? FullName { get; set; }
            public DateTime? DateOfBirth { get; set; }
            public string? Gender { get; set; }
            public string? Email { get; set; }
            public string? PhoneNumber { get; set; }
            public string? City { get; set; }
        }
    }
}
