using NewMicroservice.File.Api.Features.File.Upload;

namespace NewMicroservice.File.Api.Features.File.Delete
{
    public static class DeleteFileCommandEndpoint
    {
        public static RouteGroupBuilder DeleteFileGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/{filename}", async (string filename, IMediator mediator) =>
            {
                return (await mediator.Send(new DeleteFileCommand(filename))).ToGenericResult();

            }).WithName("DeleteFile")
            .MapToApiVersion(1.0).DisableAntiforgery();
            return group;
        }
    }
}
