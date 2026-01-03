using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewMicroservice.Web.Pages.Instructor.ViewModel;
using NewMicroservice.Web.Services.Refit;

namespace NewMicroservice.Web.Pages.Instructor
{
    public class CoursesModel(CatalogService catalogService) : PageModel
    {
        public List<CourseViewModel> CourseViewModels { get; set; } = null!;
        public async Task OnGet()
        {
            var result = await catalogService.GetCoursesByUserId();
            if (result.IsFailed)
            {
                //TODO: Handle error
            }
            CourseViewModels = result.Data!;
        }
        public async Task<IActionResult> OnGetDeleteAsync(Guid id)
        {
            var result = await catalogService.DeleteCourse(id);
            if (result.IsFailed)
            {
                //TODO: Handle error
            }
            return RedirectToPage();
        }
    }
}
