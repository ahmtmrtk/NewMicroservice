using NewMicroservice.Catalog.Api.Features.Categories.Dto;
using NewMicroservice.Catalog.Api.Repositories;


namespace NewMicroservice.Catalog.Api.Features.Categories.GetById
{
    public class GetByIdCategoryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetByIdCategoryQuery, ServiceResult<CategoryDto>>
    {
        public async Task<ServiceResult<CategoryDto>> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await context.Categories.FindAsync(request.Id, cancellationToken);
            if (category == null)
            {
                return ServiceResult<CategoryDto>.
                    Error("Category not found", HttpStatusCode.NotFound);
            }
            var categoryDto = mapper.Map<CategoryDto>(category);
            return ServiceResult<CategoryDto>.SuccessAsOk(categoryDto);
        }
    }
}
