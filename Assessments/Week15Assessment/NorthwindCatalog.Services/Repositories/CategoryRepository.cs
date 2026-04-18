using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NorthwindCatalog.Services.Data;
using NorthwindCatalog.Services.Dtos;
using NorthwindCatalog.Services.Repositories.Interfaces;

namespace NorthwindCatalog.Services.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly NorthwindContext _context;
    private readonly IMapper _mapper;

    public CategoryRepository(NorthwindContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var categories = await _context.Categories.ToListAsync();
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }
}
