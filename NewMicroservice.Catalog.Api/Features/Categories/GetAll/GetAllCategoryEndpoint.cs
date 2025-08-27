

namespace NewMicroservice.Catalog.Api.Features.Categories.GetAll
{
    public static class GetAllCategoryEndpoint
    {
        public static RouteGroupBuilder GetAllCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) => (await mediator.Send(new GetAllCategoryQuery())).ToGenericResult())
                .MapToApiVersion(1.0)
                .WithName("GetAllCategory")
                .Produces<List<CategoryDto>>(StatusCodes.Status200OK);


            return group;
        }
    }
}
