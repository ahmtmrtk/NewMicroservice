using MassTransit;
using Microsoft.Extensions.FileProviders;
using NewMicroservice.Bus.Events;

namespace NewMicroservice.Bus.Commands
{
    public class UploadCoursePictureCommandCounsumer(IServiceProvider serviceProvider) : IConsumer<UploadCoursePictureCommand>
    {
        public async Task Consume(ConsumeContext<UploadCoursePictureCommand> context)
        {
            using var scope = serviceProvider.CreateScope();
            var fileProvider = scope.ServiceProvider.GetRequiredService<IFileProvider>();

            var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(context.Message.fileName)}";
            var uploadPath = Path.Combine(fileProvider.GetFileInfo("files").PhysicalPath!, newFileName);


            await System.IO.File.WriteAllBytesAsync(uploadPath, context.Message.picture);
            var publishedEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
            await publishedEndpoint.Publish(new CoursePictureUploadedEvent(context.Message.courseId, newFileName));

        }
    }
}