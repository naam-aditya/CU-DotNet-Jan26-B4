using Vagabond.Mvc.Models;

namespace Vagabond.Mvc.Services
{
    public class DestinationService : IDestinationService
    {
        private readonly HttpClient _client;
        public DestinationService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Destination>> GetAllAsync()
        {
            var response = await _client.GetAsync("api/destinations");

            response.EnsureSuccessStatusCode();
            var destinations = await response.Content.ReadFromJsonAsync<IEnumerable<Destination>>()
                ?? [];

            return destinations;
        }
    }
}