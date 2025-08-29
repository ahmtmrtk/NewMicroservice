using Asp.Versioning.Builder;
using NewMicroservice.Catalog.Api.Features.Categories.Create;
using NewMicroservice.Catalog.Api.Features.Courses.Create;
using NewMicroservice.Catalog.Api.Features.Courses.Delete;
using NewMicroservice.Catalog.Api.Features.Courses.GetAll;
using NewMicroservice.Catalog.Api.Features.Courses.GetAllByUser;
using NewMicroservice.Catalog.Api.Features.Courses.GetById;
using NewMicroservice.Catalog.Api.Features.Courses.Update;

namespace NewMicroservice.Catalog.Api.Features.Courses
{
    public static class CourseEndpointExt
    {
        public static void AddCourseGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/courses").WithTags("Courses").WithApiVersionSet(apiVersionSet).CreateCourseGroupItemEndpoint().GetAllCourseGroupItemEndpoint()
            .GetByIdCourseGroupItemEndpoint().UpdateCourseGroupItemEndpoint().DeleteCourseGroupItemEndpoint().GetAllCourseByUserGroupItemEndpoint();
        }
    }
}
