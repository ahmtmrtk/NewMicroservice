namespace NewMicroservice.Discount.Api.Features.Discounts.CreateDiscount
{
    public class CreateDiscountCommandValidator : AbstractValidator<CreateDiscountCommand>
    {
        public CreateDiscountCommandValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code is required.")
                .MaximumLength(10).WithMessage("Code must not exceed 10 characters.");
            RuleFor(x => x.Rate)
                .NotEmpty().WithMessage("Rate is required.")
                ;
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .Must(id => id != Guid.Empty).WithMessage("UserId must be a valid GUID.");
            RuleFor(x => x.ExpireDate)
                .NotEmpty().WithMessage("ExpireDate is required.")
                .GreaterThan(DateTime.UtcNow).WithMessage("ExpireDate must be a future date.");
        }
    }
}
