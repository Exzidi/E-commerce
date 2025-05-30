using AutoMapper;
using LIBRARY.Shared.DTO.CategoryDTO;
using LIBRARY.Shared.DTO.ProductDTO;
using LIBRARY.Shared.Entity;

namespace BACK_END.Mapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            // Mapeo de CreateCategoryDto a Category
            CreateMap<CreateCategoryDto, Category>()
                .ForMember(dest => dest.ProdCategories, opt => opt.MapFrom(src => src.ProdCategories));

            // Mapeo de CreateProdCategoryDto a ProdCategory
            CreateMap<CreateProdCategoryDto, ProdCategory>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

            // Mapeo de CreateProductDto a Product
            CreateMap<CreateProductDto, Product>()
                .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.ProductImage));

            // Mapeo de CreateProductImageDto a ProductImage
            CreateMap<CreateProductImageDto, ProductImage>();

            CreateMap<Category, CategoryResponseDto>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<ProdCategory, ProdCategoryResponseDto>();
            CreateMap<Product, ProductResponseDto>();
            CreateMap<ProductImage, ProductImageResponseDto>();
        }
    }
}
