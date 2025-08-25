using NewMicroservice.Catalog.Api.Features.Categories.Dto;
using NewMicroservice.Catalog.Api.Repositories;


namespace NewMicroservice.Catalog.Api.Features.Categories.GetAll
{
    public class GetAllCategoryHandler(AppDbContext context,IMapper mapper) : IRequestHandler<GetAllCategoryQuery, ServiceResult<List<CategoryDto>>>
    {
        public async Task<ServiceResult<List<CategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = await context.Categories                
                .ToListAsync(cancellationToken);
            var categoriesDto = mapper.Map<List<CategoryDto>>(categories);
            return ServiceResult<List<CategoryDto>>.SuccessAsOk(categoriesDto);
        }
    }
    
}
