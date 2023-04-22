using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ResortMan.MvcApp.Areas.Staff.Controllers;
[Authorize(Roles = "Staff")]
[Area("Staff")]
public class StaffController : Controller
{
	public IActionResult Index()
	{
		return View();
	}
}
