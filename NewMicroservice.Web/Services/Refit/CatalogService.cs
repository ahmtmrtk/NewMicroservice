using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewMicroservice.Web.Pages.Instructor.ViewModel;
using Refit;
using System.Text.Json;

namespace NewMicroservice.Web.Services.Refit
{
    public class CatalogService(ICatalogRefitService catalogRefitService, ILogger<CatalogService> logger)
    {

        public async Task<ServiceResult<List<CategoryViewModel>>> GetCategoriesAsync()
        {

            var response = await catalogRefitService.GetCategoriesAsync();
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<Microsoft.AspNetCore.Mvc.ProblemDetails>(response.Error.Content!);
                logger.LogError("Error occurred while fetching categories");
                return ServiceResult<List<CategoryViewModel>>.Error("Fail to retrieve categories. Please try again later");
            }

            var categories = response!.Content!
                .Select(c => new CategoryViewModel(c.Id, c.Name))
                .ToList();
            return ServiceResult<List<CategoryViewModel>>.Success(categories);
        }

        public async Task<ServiceResult> CreateCourseAsync(CreateCourseViewModel createCourseViewModel)
        {

            StreamPart? pictureStreamPart = null;
            await using var stream = createCourseViewModel.PictureFormFile?.OpenReadStream();
            if (createCourseViewModel.PictureFormFile is not null && createCourseViewModel.PictureFormFile.Length > 0)
            {
                
                pictureStreamPart = new StreamPart(stream!, createCourseViewModel.PictureFormFile.FileName, createCourseViewModel.PictureFormFile.ContentType);
            }

            var response = await catalogRefitService.CreateCourseAsync(Name: createCourseViewModel.Name,
                                                                       Description: createCourseViewModel.Description,
                                                                       Price: createCourseViewModel.Price,
                                                                       CategoryId: createCourseViewModel.CategoryId.ToString()!,
                                                                       Picture: pictureStreamPart);

            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<Microsoft.AspNetCore.Mvc.ProblemDetails>(response.Error.Content!);
                logger.LogError("Error occurred while creating a course");
                return ServiceResult.Error("Fail to create a course. Please try again later");
            }
            return ServiceResult.Success();

        }

    }
}

