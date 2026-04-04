using Vagabond.Mvc.Models;

namespace Vagabond.Mvc.Services
{
    public interface IDestinationService
    {
        Task<IEnumerable<Destination>> GetAllAsync();
    }
}