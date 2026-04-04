using Vagabond.TravelDestinationService.Models;
using Vagabond.TravelDestinationService.Repositories;
using Vagabond.TravelDestinationService.Services;
using Vagabond.TravelDestinationService.Exceptions;
using Vagabond.TravelDestinationService.Dtos;

namespace Vagabond.TravelDestinationService
{
    public class DestinationService : IDestinationService
    {
        private readonly IDestinationRepository _repository;
        public DestinationService(IDestinationRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> AddAsync(CreateDestinationDto dto)
        {
            await _repository.AddAsync(
                new Destination
                {
                    CityName = dto.CityName,
                    Country = dto.Country,
                    Description = dto.Description,
                    Rating = dto.Rating,
                    LastVisited = DateTime.UtcNow
                }
            );
            return "Destination created.";
        }

        public async Task<string> DeleteAsync(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return "Destination Deleted.";
            }
            catch (DestinationNotFoundException ex)
            {
                return ex.Message;
            }
        }

        public async Task<IList<Destination>> GetAllAsync()
            => await _repository.GetAllAsync();

        public async Task<Destination?> GetByIdAsync(int id)
        {
            try
            {
                var destination = await _repository.GetByIdAsync(id);
                return destination;
            }
            catch (DestinationNotFoundException)
            {
                return null;
            }
        }

        public async Task<string> UpdateAsync(Destination destination)
        {
            try
            {
                await _repository.UpdateAsync(destination);
                return "Destination Updated";
            }
            catch (DestinationNotFoundException ex)
            {
                return ex.Message;
            }
        }
    }
}
