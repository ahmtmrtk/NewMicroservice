using NewMicroservice.Basket.Api.Dto;

namespace NewMicroservice.Basket.Api.Features.Baskets.GetBasket
{
    public static class GetBasketEndpoint
    {
        public static RouteGroupBuilder GetBasketGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) => (await mediator.Send(new GetBasketQuery())).ToGenericResult())
                .WithName("GetBasketDto")
                .MapToApiVersion(1.0)
                .Produces<List<BasketDto>>(StatusCodes.Status200OK);


            return group;
        }
    }
}
