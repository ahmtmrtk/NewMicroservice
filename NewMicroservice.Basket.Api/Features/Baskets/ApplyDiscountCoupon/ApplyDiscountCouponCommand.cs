namespace NewMicroservice.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public record ApplyDiscountCouponCommand(string Coupon, float Rate) : IRequestByServiceResult;

}
