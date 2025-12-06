using NewMicroservice.Catalog.Api.Features.Courses.Create;

namespace NewMicroservice.Catalog.Api.Features.Courses.Update
{
    public static class UpdateCourseCommandEndpoint
    {
        public static RouteGroupBuilder UpdateCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/", async (UpdateCourseCommand command, IMediator mediator) =>
            {
                return (await mediator.Send(command)).ToGenericResult();

            }).WithName("UpdateCourse")
            .Produces<Guid>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .AddEndpointFilter<ValidationFilter<UpdateCourseCommand>>().RequireAuthorization(policyNames: "InstructorPolicy");
            return group;
        }
    }
}
