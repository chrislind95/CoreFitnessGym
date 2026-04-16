using Application.Abstractions.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebbApp.Areas.Authentication.Models;
using Presentation.WebbApp.Services;

namespace Presentation.WebbApp.Areas.Authentication.Controllers
{
    [Area("Authentication")]
    [Route("sign-in")]
    public class SignInController(IAuthService authService) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var redirectPath = AuthenticationRedirectService.GetRedirectPathWhenSignedIn(User);
            if (redirectPath is not null)
                return Redirect(redirectPath);

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

            var redirectPath = AuthenticationRedirectService.GetRedirectPathWhenSignedIn(User);
            if(redirectPath is not null)
                return Redirect(redirectPath);

            return Redirect("/");
        }
    }
}
