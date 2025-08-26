using NewMicroservice.Catalog.Api.Features.Courses.Dto;

namespace NewMicroservice.Catalog.Api.Features.Courses.GetById
{
    public record GetByIdCourseQuery(Guid Id) : IRequestByServiceResult<CourseDto>;
    
}