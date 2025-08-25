using NewMicroservice.Catalog.Api.Features.Categories.Dto;

namespace NewMicroservice.Catalog.Api.Features.Categories
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();

        }

    }
}
