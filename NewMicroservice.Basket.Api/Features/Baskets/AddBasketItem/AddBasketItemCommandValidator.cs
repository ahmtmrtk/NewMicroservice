using FluentValidation;

namespace NewMicroservice.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketItemCommandValidator : AbstractValidator<AddBasketItemCommand>
    {
        public AddBasketItemCommandValidator()
        {
            RuleFor(x => x.CourseId).NotEmpty();
            RuleFor(x => x.CourseName).NotEmpty();
            RuleFor(x => x.CoursePrice).GreaterThan(0).WithMessage("Price must be greater than zero");
            RuleFor(x => x.ImageUrl).MaximumLength(250);
        }
    }
}
