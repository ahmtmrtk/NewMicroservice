namespace NewMicroservice.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public class ApplyDiscountCouponCommandValidator : AbstractValidator<ApplyDiscountCouponCommand>
    {
        public ApplyDiscountCouponCommandValidator()
        {
            RuleFor(x => x.Coupon).NotEmpty().WithMessage("Coupon is required.");
            RuleFor(x => x.Rate).GreaterThan(0).WithMessage("Rate must be greater than 0.");
        }
    }
}
