using NewMicroservice.Catalog.Api.Features.Courses.Dto;
using NewMicroservice.Catalog.Api.Repositories;

namespace NewMicroservice.Catalog.Api.Features.Courses.GetById
{
    public class GetByIdCourseHandler(AppDbContext context) : IRequestHandler<GetByIdCourseQuery, ServiceResult<CourseDto> >
    {


        async Task<ServiceResult<CourseDto>> IRequestHandler<GetByIdCourseQuery, ServiceResult<CourseDto>>.Handle(GetByIdCourseQuery request, CancellationToken cancellationToken)
        {
            var course = await context.Courses.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (course == null)
            {
                return ServiceResult<CourseDto>.Error("Course not found", HttpStatusCode.NotFound);
            }
                
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == course.CategoryId, cancellationToken);
            if (category == null)
            {
                return ServiceResult<CourseDto>.Error("Category not found", HttpStatusCode.NotFound);
            }
            course.Category = category;
            var courseDto = new CourseDto(
                course.Id,
                course.Name,
                course.Description,
                course.Price,
                course.UserId,
                new CategoryDto(course.Category.Id, course.Category.Name),
                new FeatureDto(course.Feature.Duration, course.Feature.Rating, course.Feature.EducatorFullName)
            );
            return ServiceResult<CourseDto>.SuccessAsOk(courseDto);
            
        }
    }
}