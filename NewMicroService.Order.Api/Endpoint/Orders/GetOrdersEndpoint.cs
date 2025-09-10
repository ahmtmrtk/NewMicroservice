using MediatR;
using NewMicroservice.Shared.Extensions;
using NewMicroService.Order.Application.Features.Orders.GetOrders;

namespace NewMicroService.Order.Api.Endpoint.Orders
{
    public static class GetOrdersEndpoint
    {
        public static RouteGroupBuilder GetOrdersGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) => (await mediator.Send(new GetOrdersQuery())).ToGenericResult())
                .WithName("GetOrders")
                .MapToApiVersion(1.0)
                .Produces<List<GetOrdersResponse>>(StatusCodes.Status200OK);


            return group;
        }
    }
}
