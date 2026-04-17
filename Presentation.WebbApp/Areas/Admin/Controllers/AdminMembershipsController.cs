using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebbApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
[Route("admin")]
public class AdminMembershipsController : Controller
{
    [HttpGet("memberships")]
    public IActionResult Index()
    {
        return View();
    }
}
