using NewMicroservice.Basket.Api.Features.Baskets.AddBasketItem;

namespace NewMicroservice.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public static class ApplyDiscountCouponCommandEndpoint
    {
        public static RouteGroupBuilder ApplyDiscountCouponItemGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/apply-discount-rate", async (ApplyDiscountCouponCommand command, IMediator mediator) =>
            {
                return (await mediator.Send(command)).ToGenericResult();

            }).WithName("ApplyDiscountRate")
            .MapToApiVersion(1.0)
            .AddEndpointFilter<ValidationFilter<ApplyDiscountCouponCommand>>();
            return group;
        }
    }
}
