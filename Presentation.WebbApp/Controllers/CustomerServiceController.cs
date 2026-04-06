using Application.Abstractions.Services;
using Application.CustomerService;
using Application.CustomerService.Inputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebbApp.Models.CustomerService;

namespace Presentation.WebbApp.Controllers
{
    [Route("support")]
    public class CustomerServiceController(IContactRequestService contactRequestService) : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactForm form)
        {
            if(!ModelState.IsValid)
                return View(form);

            var input = new ContactRequestInput(
                form.FirstName,
                form.LastName,
                form.Email,
                form.Phone,
                form.Message
            );

            var result = await contactRequestService.CreateContactRequestAsync(input);

            if (!result)
            {
                TempData["ErrorMessage"] = "Unable to send contact request";
                return View(form);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
