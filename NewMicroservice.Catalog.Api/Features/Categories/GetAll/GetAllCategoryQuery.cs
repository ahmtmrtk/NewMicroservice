using Amazon.Runtime.Internal;
using MediatR;
using NewMicroservice.Catalog.Api.Features.Categories.Dto;
using NewMicroservice.Shared;

namespace NewMicroservice.Catalog.Api.Features.Categories.GetAll
{
    public class GetAllCategoryQuery : IRequestByServiceResult<List<CategoryDto>>
    {

    }
}
