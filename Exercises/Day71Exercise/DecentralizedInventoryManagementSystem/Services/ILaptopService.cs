using DecentralizedInventoryManagementSystem.Models;

namespace DecentralizedInventoryManagementSystem.Services
{
    public interface ILaptopService
    {
        Task<IList<Laptop>> GetLaptopsAsync();
        Task CreateLaptopAsync(Laptop laptop);
    }
}