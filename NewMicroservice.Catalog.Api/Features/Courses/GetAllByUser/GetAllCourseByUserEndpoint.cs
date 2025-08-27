using NewMicroservice.Catalog.Api.Features.Courses.Dto;

namespace NewMicroservice.Catalog.Api.Features.Courses.GetAllByUser
{
    public static class GetAllCourseByUserEndpoint
    {
        public static RouteGroupBuilder GetAllCourseByUserGroupItemEndpoint(this RouteGroupBuilder routeGroupBuilder)
        {
            routeGroupBuilder.MapGet("/user/{userId:guid}", async (IMediator mediator, Guid userId) => (await mediator.Send(new GetAllCourseByUserQuery(userId))).ToGenericResult())
                .WithName("GetAllCourseByUser")
                .Produces<List<CourseDto>>(StatusCodes.Status200OK);
            return routeGroupBuilder;
        }
    }
}
