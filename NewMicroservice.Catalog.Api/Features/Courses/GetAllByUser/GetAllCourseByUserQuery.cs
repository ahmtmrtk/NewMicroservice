using NewMicroservice.Catalog.Api.Features.Courses.Dto;

namespace NewMicroservice.Catalog.Api.Features.Courses.GetAllByUser
{
    public record GetAllCourseByUserQuery(Guid UserId) : IRequestByServiceResult<List<CourseDto>>;


}
