namespace NewMicroservice.File.Api.Features.File.Upload
{
    public static class UploadFileCommandEndpoint
    {
        public static RouteGroupBuilder UploadFileGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (IFormFile file, IMediator mediator) =>
            {
                return (await mediator.Send(new UploadFileCommand(file))).ToGenericResult();

            }).WithName("UploadFile")
            .MapToApiVersion(1.0).DisableAntiforgery();
            return group;
        }
    }
}
