
namespace NewMicroservice.Catalog.Api.Features.Categories.GetById
{
    public static class GetByIdCategoryEndpoint
    {
        public static RouteGroupBuilder GetByIdCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}", async (Guid id, IMediator mediator) => (await mediator.Send(new GetByIdCategoryQuery(id))).ToGenericResult()).WithName("GetByIdCategory")
                .Produces<CategoryDto>(StatusCodes.Status200OK);
            return group;
        }
    }
}
