using Microsoft.AspNetCore.Mvc;
using NewMicroservice.Web.Pages.Instructor.Dto;
using Refit;

namespace NewMicroservice.Web.Services.Refit
{
    public interface ICatalogRefitService
    {
        [Get("/api/v1/categories")]
        Task<ApiResponse<List<CategoryDto>>> GetCategoriesAsync();

        [Get("/api/v1/courses/user/{userId}")]
        Task<ApiResponse<List<CourseDto>>> GetCoursesByUserId(Guid UserId);

        [Multipart]
        [Post("/api/v1/courses")]
        Task<ApiResponse<object>> CreateCourseAsync(
            [AliasAs("Name")] string Name,
            [AliasAs("Description")] string Description,
            [AliasAs("Price")] decimal Price,
            [AliasAs("Picture")] StreamPart? Picture,
            [AliasAs("CategoryId")] string CategoryId);

        [Get("/api/v1/courses")]
        Task<ApiResponse<List<CourseDto>>> GetAllCourses();

        [Get("/api/v1/courses/{id}")]
        Task<ApiResponse<CourseDto>> GetCourse(Guid id);

        [Put("/api/v1/courses")]
        Task<ApiResponse<object>> UpdateCourseAsync(UpdateCourseRequest request);


        [Delete("/api/v1/courses/{id}")]
        Task<ApiResponse<object>> DeleteCourseAsync(Guid id);
    }
}
