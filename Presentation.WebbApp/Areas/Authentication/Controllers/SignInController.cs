using Application.Abstractions.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebbApp.Areas.Authentication.Models;

namespace Presentation.WebbApp.Areas.Authentication.Controllers
{
    [Area("Authentication")]
    [Route("sign-in")]
    public class SignInController(IAuthService authService) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var redirect = RedirectWhenSignedIn;
            if (redirect is not null)
                return (redirect);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SignInForm form)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(nameof(form.ErrorMessage), "Incorrect email address or password");
                return View(form);
            }

            var result = await authService.LoginUserAsync(form.Email, form.Password, form.RememberMe);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(nameof(form.ErrorMessage), result?.ErrorMessage ?? "Incorrect email address or password");
                return View(form);
            }

            var redirect = RedirectWhenSignedIn;
            if(redirect is not null)
                return (redirect);

            return Redirect("/");
        }

        private IActionResult? RedirectWhenSignedIn
        {
            get
            {
                if(User.Identity?.IsAuthenticated == true)
                {
                    if (User.IsInRole("Admin"))
                        return Redirect("/admin");

                    if (User.IsInRole("Member"))
                        return Redirect("/me");

                    return Redirect("/");
                }
                return null;
            }
        }
    }
}
