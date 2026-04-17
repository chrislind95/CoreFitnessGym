using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebbApp.Controllers;

public class ErrorController : Controller
{
    [HttpGet("404")]
    public IActionResult Index()
    {
        return View();
    }
}
