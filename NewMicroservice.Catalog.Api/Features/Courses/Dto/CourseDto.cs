namespace NewMicroservice.Catalog.Api.Features.Courses.Dto
{
    public record CourseDto(Guid Id, string Name, string Description, decimal Price, Guid UserId, CategoryDto Category, FeatureDto Feature);

}
