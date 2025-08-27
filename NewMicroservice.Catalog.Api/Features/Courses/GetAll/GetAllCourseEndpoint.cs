using NewMicroservice.Catalog.Api.Features.Categories.GetAll;
using NewMicroservice.Catalog.Api.Features.Courses.Dto;

namespace NewMicroservice.Catalog.Api.Features.Courses.GetAll
{
    public static class GetAllCourseEndpoint
    {
        public static RouteGroupBuilder GetAllCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) => (await mediator.Send(new GetAllCourseQuery())).ToGenericResult())
                .WithName("GetAllCourse")
                .MapToApiVersion(1.0)
                .Produces<List<CategoryDto>>(StatusCodes.Status200OK);


            return group;
        }
    }
}
