using NorthwindCatalog.Services.Dtos;

namespace NorthwindCatalog.Services.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<CategoryDto>> GetAllAsync();
}
