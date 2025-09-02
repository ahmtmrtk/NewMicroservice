namespace NewMicroservice.File.Api.Features.File.Delete
{
    public record DeleteFileCommand(string FileName) : IRequestByServiceResult;

}
