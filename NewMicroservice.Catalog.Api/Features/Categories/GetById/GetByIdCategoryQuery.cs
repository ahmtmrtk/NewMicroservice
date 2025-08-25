
namespace NewMicroservice.Catalog.Api.Features.Categories.GetById
{
    public record GetByIdCategoryQuery(Guid Id) : IRequestByServiceResult<CategoryDto>;
}