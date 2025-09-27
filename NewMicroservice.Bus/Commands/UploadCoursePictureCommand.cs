namespace NewMicroservice.Bus.Commands
{
    public record UploadCoursePictureCommand(Guid courseId, Byte[] picture,string fileName);
}