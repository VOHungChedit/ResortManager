using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ResortMan.MvcApp.Areas.Dashboard.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Dashboard")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
