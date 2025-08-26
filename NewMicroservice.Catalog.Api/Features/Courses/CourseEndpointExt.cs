using NewMicroservice.Catalog.Api.Features.Categories.Create;
using NewMicroservice.Catalog.Api.Features.Courses.Create;
using NewMicroservice.Catalog.Api.Features.Courses.Delete;
using NewMicroservice.Catalog.Api.Features.Courses.GetAll;
using NewMicroservice.Catalog.Api.Features.Courses.GetById;
using NewMicroservice.Catalog.Api.Features.Courses.Update;

namespace NewMicroservice.Catalog.Api.Features.Courses
{
    public static class CourseEndpointExt
    {
        public static void AddCourseGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/courses").WithTags("Courses").CreateCourseGroupItemEndpoint().GetAllCourseGroupItemEndpoint()
            .GetByIdCourseGroupItemEndpoint().UpdateCourseGroupItemEndpoint().DeleteCourseGroupItemEndpoint();
        }
    }
}
