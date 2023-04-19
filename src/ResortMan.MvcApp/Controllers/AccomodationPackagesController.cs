using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResortMan.Entities;
using ResortMan.MvcApp.Areas.Dashboard.ViewModels;
using ResortMan.Services;
using System.IO.Compression;
using System.IO;

namespace ResortMan.MvcApp.Controllers;

public class AccomodationPackagesController : Controller
{
    private readonly AccomodationPackagesService accomodationPackagesService;
    private readonly AccomodationTypesService accomodationTypesService;

    public AccomodationPackagesController(AccomodationPackagesService accomodationPackagesService,
        AccomodationTypesService accomodationTypesService)
    {
        this.accomodationPackagesService = accomodationPackagesService;
        this.accomodationTypesService = accomodationTypesService;
    }
    
    public IActionResult Details(int? id)
    {
        if (id == null)
            return BadRequest();

        var model = accomodationPackagesService.GetAccomodationPackageById(id.Value);

        if (model == null)
            return NotFound();

        return View(model);
    }
   
}
