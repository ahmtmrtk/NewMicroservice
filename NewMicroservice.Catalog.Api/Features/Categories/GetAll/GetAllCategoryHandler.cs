using MediatR;
using Microsoft.EntityFrameworkCore;
using NewMicroservice.Catalog.Api.Features.Categories.Dto;
using NewMicroservice.Catalog.Api.Repositories;
using NewMicroservice.Shared;
using System.Linq;

namespace NewMicroservice.Catalog.Api.Features.Categories.GetAll
{
    public class GetAllCategoryHandler(AppDbContext context) : IRequestHandler<GetAllCategoryQuery, ServiceResult<List<CategoryDto>>>
    {
        public async Task<ServiceResult<List<CategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = await context.Categories
                .Select(c => new CategoryDto(c.Id, c.Name))
                .ToListAsync(cancellationToken);
            return ServiceResult<List<CategoryDto>>.SuccessAsOk(categories);
        }
    }
    
}
