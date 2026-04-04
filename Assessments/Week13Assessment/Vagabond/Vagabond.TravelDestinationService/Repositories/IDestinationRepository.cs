using Vagabond.TravelDestinationService.Models;

namespace Vagabond.TravelDestinationService.Repositories
{
    public interface IDestinationRepository
    {
        Task<IList<Destination>> GetAllAsync();
        Task<Destination> GetByIdAsync(int id);
        Task AddAsync(Destination destination);
        Task UpdateAsync(Destination destination);
        Task DeleteAsync(int id);
    }
}