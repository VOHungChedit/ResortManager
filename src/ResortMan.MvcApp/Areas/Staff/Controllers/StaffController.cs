using Microsoft.AspNetCore.Mvc;

namespace ResortMan.MvcApp.Areas.Staff.Controllers;
[Area("Staff")]
public class StaffController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
