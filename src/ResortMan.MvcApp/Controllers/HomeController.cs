using Microsoft.AspNetCore.Mvc;
using ResortMan.MvcApp.Models;
using ResortMan.MvcApp.ViewModels;
using ResortMan.Services;
using System.Diagnostics;

namespace ResortMan.MvcApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AccomodationPackagesService accomodationPackagesService;

    public HomeController(ILogger<HomeController> logger,
        AccomodationPackagesService accomodationPackagesService)
    {
        _logger = logger;
        this.accomodationPackagesService = accomodationPackagesService;
    }

    public IActionResult Index()
    {
        var model = new HomeIndexViewModel()
        {
            AccomodationPackages = accomodationPackagesService.GetPromote(6)
        };

        return View(model);
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}