
using NewMicroservice.Catalog.Api.Repositories;

namespace NewMicroservice.Catalog.Api.Features.Courses.Update
{
    public class UpdateCourseHandler(AppDbContext context, IMapper mapper) : IRequestHandler<UpdateCourseCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await context.Courses.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (course == null)
            {
                return ServiceResult.Error("Course not found", HttpStatusCode.NotFound);
            }
            var category = context.Categories.FirstOrDefaultAsync(x => x.Id == request.CategoryId);
            if (category == null)
            {
                return ServiceResult.Error("Category not found", HttpStatusCode.NotFound);
            }
            course.Name = request.Name;
            course.Description = request.Description;
            course.Price = request.Price;
            course.CategoryId = request.CategoryId;
            context.Courses.Update(course);
            context.SaveChanges();
            return ServiceResult.SuccessAsNoContent();
        }
    }
}
