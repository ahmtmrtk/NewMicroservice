using NewMicroservice.Discount.Api.Features.Discounts.CreateDiscount;
using NewMicroservice.Discount.Api.Features.Discounts.Dto;

namespace NewMicroservice.Discount.Api.Features.Discounts.GetByCode
{
    public static class GetByCodeDiscountEndpoint
    {
        public static RouteGroupBuilder GetByCodeDiscountGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{code:length(10)}", async (IMediator mediator, string code) => (await mediator.Send(new GetByCodeDiscountQuery(code))).ToGenericResult())
                .WithName("GetByCodeDiscount")
                .MapToApiVersion(1.0)
                .Produces<DiscountDto>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);

            return group;
        }
    }
}
