using Amazon.Runtime.Internal;
using MediatR;
using NewMicroservice.Shared;

namespace NewMicroservice.Catalog.Api.Features.Categories.Create
{
    public record CreateCategoryCommand(string Name):IRequestByServiceResult<CreateCategoryResponse>;

}
