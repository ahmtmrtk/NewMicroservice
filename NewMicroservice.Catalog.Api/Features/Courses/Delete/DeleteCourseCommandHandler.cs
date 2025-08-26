
using NewMicroservice.Catalog.Api.Repositories;

namespace NewMicroservice.Catalog.Api.Features.Courses.Delete
{
    public class DeleteCourseCommandHandler(AppDbContext context) : IRequestHandler<DeleteCourseCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await context.Courses.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (course == null)
            {
                return ServiceResult.Error("Course not found", HttpStatusCode.NotFound);
            }
            context.Courses.Remove(course);
            context.SaveChanges();
            return ServiceResult.SuccessAsNoContent();
        }
    }
}
