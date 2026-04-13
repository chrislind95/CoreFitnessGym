using Application.Abstractions.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebbApp.Areas.Authentication.Models;

namespace Presentation.WebbApp.Areas.Authentication.Controllers
{
    [Area("Authentication")]
    public class SignUpController(IAuthService authService) : Controller
    {

        [HttpGet("sign-up")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUpForm form)
        {
            if (!ModelState.IsValid)
                return View(form);

            HttpContext.Session.SetString("reg_email", form.Email);
            return RedirectToAction(nameof(SetPassword));
        }

        [HttpGet("set-password")]
        public IActionResult SetPassword()
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session?.GetString("reg_email")))
                return RedirectToAction(nameof(Index));

            return View();
        }

        [HttpPost("set-password")]
        public async Task<IActionResult> SetPassword(SetPasswordForm form)
        {
            var email = HttpContext.Session?.GetString("reg_email");

            if (string.IsNullOrWhiteSpace(email))
                return RedirectToAction(nameof(Index));

            if (!ModelState.IsValid)
                return View(form);

            var created = await authService.CreateUserAsync(email, form.Password, "Member");
            if (!created.Succeeded)
            {
                ModelState.AddModelError(nameof(form.ErrorMessage), created.ErrorMessage ?? "Unable to create user");
                return View(form);
            }

            var result = await authService.LoginUserAsync(email, form.Password, false);
            if (!result.Succeeded)
                return RedirectToAction(nameof(SignIn));

            return RedirectToAction("Index", "SignIn");
        }
    }
}
