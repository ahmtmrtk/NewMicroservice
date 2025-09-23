
using NewMicroservice.Catalog.Api.Repositories;


namespace NewMicroservice.Catalog.Api.Features.Categories.Create
{
    public class CreateCategoryCommandHandler(AppDbContext context) : IRequestHandler<CreateCategoryCommand, ServiceResult<CreateCategoryResponse>>
    {
        public async Task<ServiceResult<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var existCategory = await context.Categories.AnyAsync(c => c.Name == request.Name);
            if (existCategory)
            {
                ServiceResult<CreateCategoryResponse>.Error("Category with the same name already exists.", $"The category name '{request.Name}' already exists", HttpStatusCode.BadRequest);
            }

            var category = new Category
            {
                Id = Guid.CreateVersion7(),
                Name = request.Name
            };
            await context.Categories.AddAsync(category, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new CreateCategoryResponse(category.Id.ToString()), "<empty>");


        }
    }
}
