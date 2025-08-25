
namespace NewMicroservice.Catalog.Api.Features.Categories.GetById
{
    public static class GetByIdCategoryEndpoint
    {
        public static RouteGroupBuilder GetByIdCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}", async (Guid id, IMediator mediator) => (await mediator.Send(new GetByIdCategoryQuery(id))).ToGenericResult());
            return group;
        }
    }
}
