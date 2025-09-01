namespace NewMicroservice.Basket.Api.Features.Baskets.RemoveDiscountCoupon
{
    public static class RemoveDiscountCouponCommandEndpoint
    {
        public static RouteGroupBuilder RemoveDiscountCouponGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/remove-discount-coupon", async (IMediator mediator) => (await mediator.Send(new RemoveDiscountCouponCommand())).ToGenericResult()).WithName("RemoveDiscountCoupon")
                .MapToApiVersion(1.0)
                .Produces(StatusCodes.Status200OK);
            return group;
        }
    }
}
