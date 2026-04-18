using NorthwindCatalog.Services.Dtos;

namespace NorthwindCatalog.Services.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
}
