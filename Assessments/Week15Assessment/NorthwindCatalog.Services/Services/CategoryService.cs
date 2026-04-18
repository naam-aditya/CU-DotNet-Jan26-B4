using NorthwindCatalog.Services.Dtos;
using NorthwindCatalog.Services.Repositories.Interfaces;
using NorthwindCatalog.Services.Services.Interfaces;

namespace NorthwindCatalog.Services.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }
}
