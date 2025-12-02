using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewMicroservice.Web.Pages.Instructor.ViewModel;
using NewMicroservice.Web.Services.Refit;
using System.Security.Cryptography.X509Certificates;

namespace NewMicroservice.Web.Pages.Instructor
{
    public class CreateCourseModel(CatalogService catalogService) : PageModel
    {
        public CreateCourseViewModel ViewModel { get; set; } = CreateCourseViewModel.Empty;
        public async Task OnGet()
        {
            var categoriesResult = await catalogService.GetCategoriesAsync();
            if (categoriesResult.IsFailed)
            {
                // TODO: Handle the error appropriately (e.g., log it, show a message to the user, etc.)

            }

            ViewModel.SetCategoryDropdownList(categoriesResult.Data!);

        }
    }
}
