using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using ResortMan.Entities;
using ResortMan.MvcApp.Areas.Dashboard.ViewModels;
using ResortMan.Services;

namespace ResortMan.MvcApp.Areas.Dashboard.Controllers;

[Area("Dashboard")]
public class AccomodationTypesController : Controller
{
    private readonly AccomodationTypesService accomodationTypesService;

    public AccomodationTypesController(AccomodationTypesService accomodationTypesService)
    {
        this.accomodationTypesService = accomodationTypesService;
    }
    public IActionResult Index()
    {
        var list = accomodationTypesService.GetAccomodationTypes();
        AccomodationTypesListingModel model = new AccomodationTypesListingModel()
        {
            AccomodationTypes = list,
        };
        return View(model);
        //return View();
    }

    public IActionResult Listing()
    {
        AccomodationTypesListingModel model = new AccomodationTypesListingModel();
        var list = accomodationTypesService.GetAccomodationTypes();
        return PartialView("_Listing", model);
    }

    [HttpGet]
    public ActionResult Action()
    {
        AccomodationTypesActionModel model = new AccomodationTypesActionModel();
        return PartialView("_Action", model);
    }

    [HttpPost]
    public ActionResult Action(AccomodationTypesActionModel model)
    {
        AccomodationType accomodationType = new()
        {
            Name = model.Name,
            Description = model.Description,
        };

        var result = accomodationTypesService.SaveAccomodationType(accomodationType);

        object json;
        if (result)
        {
            json = new { Success = true };

        }
        else
        {
            json = new { Success = false, Message = "Unable to add Accomodation Type." };
        }

        return Json(json);

    }

}
