using NewMicroservice.Catalog.Api.Features.Courses.Dto;

namespace NewMicroservice.Catalog.Api.Features.Courses.GetById
{
    public static class GetByIdCourseEndpoint
    {
        public static RouteGroupBuilder GetByIdCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}", async (IMediator mediator, Guid id) => (await mediator.Send(new GetByIdCourseQuery(id))).ToGenericResult())
                .WithName("GetByIdCourse")
                .MapToApiVersion(1.0)
                .Produces<CourseDto>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);

            return group;
        }
    }
}