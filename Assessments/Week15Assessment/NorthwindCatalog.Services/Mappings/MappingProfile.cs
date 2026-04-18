using AutoMapper;
using NorthwindCatalog.Services.Dtos;
using NorthwindCatalog.Services.Models;

namespace NorthwindCatalog.Services.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Category mappings
        CreateMap<Category, CategoryDto>()
            .ForMember(dest => dest.ImageUrl, 
                opt => opt.MapFrom(src => GenerateImageUrl(src.CategoryId)));

        // Product mappings
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.UnitPrice,
                opt => opt.MapFrom(src => src.UnitPrice ?? 0))
            .ForMember(dest => dest.UnitsInStock,
                opt => opt.MapFrom(src => src.UnitsInStock ?? 0));
    }

    private static string GenerateImageUrl(int categoryId)
    {
        // Generate dynamic image URL based on category ID
        return $"/images/category-{categoryId}.jpg";
    }
}
