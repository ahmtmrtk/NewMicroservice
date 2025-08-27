using NewMicroservice.Catalog.Api.Features.Courses.Dto;
using NewMicroservice.Catalog.Api.Repositories;
using System.Linq;
using ZstdSharp.Unsafe;

namespace NewMicroservice.Catalog.Api.Features.Courses.GetAllByUser
{
    public class GetAllCourseByUserHandle(AppDbContext context) : IRequestHandler<GetAllCourseByUserQuery, ServiceResult<List<CourseDto>>>
    {
        public async Task<ServiceResult<List<CourseDto>>> Handle(GetAllCourseByUserQuery request, CancellationToken cancellationToken)
        {
            var categories = context.Categories.ToList();
            var courses = await context.Courses.Where(c => c.UserId == request.UserId).ToListAsync(cancellationToken);
            if (!courses.Any())
                return ServiceResult<List<CourseDto>>.SuccessAsOk(new List<CourseDto>());
            foreach (var course in courses)
            {
                course.Category = categories.FirstOrDefault(c => c.Id == course.CategoryId);
            }

            var courseDtos = courses.Select(c => new CourseDto(
                c.Id,
                c.Name,
                c.Description,
                c.Price,
                c.UserId,
                new CategoryDto(
                    c.Category.Id,
                    c.Category.Name
                ),
                new FeatureDto
                (
                    c.Feature.Duration,
                    c.Feature.Rating,
                    c.Feature.EducatorFullName
                )
            )).ToList();

            return ServiceResult<List<CourseDto>>.SuccessAsOk(courseDtos);
        }
    }
}
