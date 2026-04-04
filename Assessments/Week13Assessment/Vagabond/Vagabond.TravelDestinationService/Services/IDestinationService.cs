using Vagabond.TravelDestinationService.Dtos;
using Vagabond.TravelDestinationService.Models;

namespace Vagabond.TravelDestinationService.Services
{
    public interface IDestinationService
    {
        Task<IList<Destination>> GetAllAsync();
        Task<Destination?> GetByIdAsync(int id);
        Task<string> AddAsync(CreateDestinationDto dto);
        Task<string> UpdateAsync(Destination destination);
        Task<string> DeleteAsync(int id);

    }
}
