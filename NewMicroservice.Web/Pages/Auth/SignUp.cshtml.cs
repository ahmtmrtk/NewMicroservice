using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewMicroservice.Web.Pages.Auth.SignUp;

namespace NewMicroservice.Web.Pages.Auth
{
    public class SignUpModel(SignUpService signUpService) : PageModel
    {
        [BindProperty] public SignUpViewModel SignUpViewModel { get; set; } = SignUpViewModel.GetExampleModel;
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var result = await signUpService.CreateAccount(SignUpViewModel);
            if (result.IsFailed)
            {
                ModelState.AddModelError(string.Empty, result.Fail?.Title ?? "An error occurred during sign up.");
                if (result.Fail?.Detail != null)
                {
                    ModelState.AddModelError(string.Empty, result.Fail.Detail);
                }
                return Page();
            }

            return RedirectToPage("/Index");

        }
    }
}
