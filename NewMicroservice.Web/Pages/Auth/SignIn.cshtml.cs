using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewMicroservice.Web.Pages.Auth.SignIn;
using NewMicroservice.Web.Pages.Auth.SignUp;
using UdemyNewMicroservice.Web.Pages.Auth.SignIn;

namespace NewMicroservice.Web.Pages.Auth
{
    public class SignInModel(SignInService signInService) : PageModel
    {
        [BindProperty] public required SignInViewModel SignInViewModel { get; set; } = SignInViewModel.GetExampleModel;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var result = await signInService.AuthenticateAsync(SignInViewModel);

            if (result.IsFailed)
            {
                ModelState.AddModelError(string.Empty, result.Fail!.Title!);

                ModelState.AddModelError(string.Empty, result.Fail!.Detail!);
                return Page();
            }

            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnGetSignOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");
        }
    }
}
