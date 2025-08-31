using MediatR;
using NewMicroservice.Shared.Extensions;
using NewMicroservice.Shared.Filters;

namespace NewMicroservice.Basket.Api.Features.Baskets.AddBasketItem
{
    public static class AddBasketItemEndpoint
    {
        public static RouteGroupBuilder AddBasketItemGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/item", async (AddBasketItemCommand command, IMediator mediator) =>
            {
                return (await mediator.Send(command)).ToGenericResult();

            }).WithName("AddBasketItem")
            .MapToApiVersion(1.0)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status404NotFound)
            .AddEndpointFilter<ValidationFilter<AddBasketItemCommand>>();
            return group;
        }
    }
}
