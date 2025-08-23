using MediatR;
using NewMicroservice.Catalog.Api.Features.Categories.Dto;
using NewMicroservice.Shared.Extensions;
using NewMicroservice.Shared.Filters;

namespace NewMicroservice.Catalog.Api.Features.Categories.GetAll
{
    public static class GetAllCategoryEndpoint
    {
        public static RouteGroupBuilder GetAllCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) => (await mediator.Send(new GetAllCategoryQuery())).ToGenericResult());


            return group;
        }
    }
}
