using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryDto, Category>();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<Category, CreateCategoryDto>();

        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
        CreateMap<Product, UpdateProductDto>();

        CreateMap<Movement, MovementDto>();
        CreateMap<CreateMovementDto, Movement>();
    }
}