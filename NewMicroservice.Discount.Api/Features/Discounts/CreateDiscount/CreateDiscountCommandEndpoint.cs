namespace NewMicroservice.Discount.Api.Features.Discounts.CreateDiscount
{
    public static class CreateDiscountCommandEndpoint
    {
        public static RouteGroupBuilder CreateDiscountGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateDiscountCommand command, IMediator mediator) =>
            {
                return (await mediator.Send(command)).ToGenericResult();

            }).WithName("CreateDiscount")
            .MapToApiVersion(1.0)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status404NotFound)
            .AddEndpointFilter<ValidationFilter<CreateDiscountCommand>>();
            return group;
        }
    }
}
