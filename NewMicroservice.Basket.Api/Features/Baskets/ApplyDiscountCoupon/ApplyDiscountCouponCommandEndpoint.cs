using NewMicroservice.Basket.Api.Features.Baskets.AddBasketItem;

namespace NewMicroservice.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public static class ApplyDiscountCouponCommandEndpoint
    {
        public static RouteGroupBuilder ApplyDiscountCouponItemGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/apply-discount-coupon", async (ApplyDiscountCouponCommand command, IMediator mediator) =>
            {
                return (await mediator.Send(command)).ToGenericResult();

            }).WithName("ApplyDiscountCoupon")
            .MapToApiVersion(1.0)
            .AddEndpointFilter<ValidationFilter<ApplyDiscountCouponCommand>>();
            return group;
        }
    }
}
