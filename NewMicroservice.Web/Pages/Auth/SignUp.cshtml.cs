using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewMicroservice.Web.Pages.Auth.SignUp;

namespace NewMicroservice.Web.Pages.Auth
{
    public class SignUpModel : PageModel
    {
        [BindProperty] public SignUpViewModel SignUpViewModel { get; set; } = SignUpViewModel.Empty;
        public void OnGet()
        {
        }
    }
}
