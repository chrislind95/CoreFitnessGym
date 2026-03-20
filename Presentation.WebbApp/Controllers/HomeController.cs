using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebbApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

}
