using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebbApp.Controllers;

[Route("fitness-centers")]
public class FitnessCentersController : Controller
{
    [HttpGet("")]
    [AllowAnonymous]
    public IActionResult Index()
    {
        ViewData["Title"] = "Our Fitness Centers";
        return View();
    }
}
