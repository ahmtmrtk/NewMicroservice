using NewMicroservice.Catalog.Api.Features.Courses.Dto;
using NewMicroservice.Catalog.Api.Repositories;

namespace NewMicroservice.Catalog.Api.Features.Courses.GetAll
{
    public class GetAllCourseHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetAllCourseQuery, ServiceResult<List<CourseDto>>>
    {
        public async Task<ServiceResult<List<CourseDto>>> Handle(GetAllCourseQuery request, CancellationToken cancellationToken)
        {
            var categories = await context.Categories.ToListAsync(cancellationToken);
            var courses = await context.Courses.ToListAsync(cancellationToken);
            foreach (var course in courses)
            {
                course.Category = categories.First(c => c.Id == course.CategoryId);
            }
            var courseDtos = courses.Select(x => new CourseDto(
                x.Id,
                x.Name,
                x.Description,
                x.Price,
                x.UserId,
                new CategoryDto(x.Category.Id, x.Category.Name),
                new FeatureDto(x.Feature.Duration, x.Feature.Rating, x.Feature.EducatorFullName)
            )).ToList();
            return ServiceResult<List<CourseDto>>.SuccessAsOk(courseDtos);
        }
    }
}
