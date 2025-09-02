
using Microsoft.Extensions.FileProviders;
using System;

namespace NewMicroservice.File.Api.Features.File.Delete
{
    public class DeleteFileCommandHandler(IFileProvider provider) : IRequestHandler<DeleteFileCommand, ServiceResult>
    {
        public Task<ServiceResult> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var filePath = provider.GetFileInfo(Path.Combine("files", request.FileName));
            if (!filePath.Exists)
            {
                return Task.FromResult(ServiceResult.ErrorAsNotFound());
            }
            System.IO.File.Delete(filePath.PhysicalPath!);
            return Task.FromResult(ServiceResult.SuccessAsNoContent());
        }
    }
}
