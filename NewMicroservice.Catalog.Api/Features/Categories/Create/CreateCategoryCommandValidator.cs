

namespace NewMicroservice.Catalog.Api.Features.Categories.Create
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Name).Length(4,25).WithMessage("Name must between 4-25 characters.");
        }
    }
}