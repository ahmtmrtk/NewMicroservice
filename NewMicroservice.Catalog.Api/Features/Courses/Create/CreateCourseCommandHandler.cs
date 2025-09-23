
using NewMicroservice.Catalog.Api.Repositories;

namespace NewMicroservice.Catalog.Api.Features.Courses.Create
{
    public class CreateCourseCommandHandler(AppDbContext context, IMapper mapper) : IRequestHandler<CreateCourseCommand, ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var category = await context.Categories.AnyAsync(c => c.Id == request.CategoryId, cancellationToken);
            if (!category)
            {
                return ServiceResult<Guid>.Error("Category Not Found", HttpStatusCode.NotFound);
            }

            var hasCourseWithSameName = await context.Courses.AnyAsync(c => c.Name == request.Name, cancellationToken);
            if (hasCourseWithSameName)
            {
                return ServiceResult<Guid>.Error("A course with the same name already exists.", HttpStatusCode.BadRequest);
            }
            var course = mapper.Map<Course>(request);
            course.CreatedDate = DateTime.Now;
            course.Id = Guid.CreateVersion7();
            course.Feature = new Feature
            {
                Duration = 10,
                Rating = 0,
                EducatorFullName = "Admin"
            };
            await context.Courses.AddAsync(course);
            await context.SaveChangesAsync(cancellationToken);
            return ServiceResult<Guid>.SuccessAsCreated(course.Id, $"/api/courses/{course.Id}");

        }
    }
}
