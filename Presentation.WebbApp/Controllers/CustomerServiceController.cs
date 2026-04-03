using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebbApp.Controllers
{
    [Route("support")]
    public class CustomerServiceController : Controller
    {
        [HttpGet("")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            ViewData["Title"] = "Customer Service";
            return View();
        }
    }
}
