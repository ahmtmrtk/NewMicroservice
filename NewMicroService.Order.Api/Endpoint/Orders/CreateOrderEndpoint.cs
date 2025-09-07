using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewMicroservice.Shared.Extensions;
using NewMicroservice.Shared.Filters;
using NewMicroService.Order.Application.Features.Orders.Create;

namespace NewMicroService.Order.Api.Endpoint.Orders
{
    public static class CreateOrderEndpoint
    {
        public static RouteGroupBuilder CreateOrderGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async ([FromBody] CreateOrderCommand command, [FromServices] IMediator mediator) =>
            {
                return (await mediator.Send(command)).ToGenericResult();

            }).WithName("CreateOrder")
            .MapToApiVersion(1.0)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status404NotFound)
            .AddEndpointFilter<ValidationFilter<CreateOrderCommandValidator>>();
            return group;
        }
    }
}
