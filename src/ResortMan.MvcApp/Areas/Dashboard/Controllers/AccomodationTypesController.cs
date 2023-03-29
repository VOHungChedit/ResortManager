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
    public ActionResult Action(int? Id)
    {
        AccomodationTypesActionModel model = new AccomodationTypesActionModel();

        if (Id.HasValue) 
        {
            var accomodationType = accomodationTypesService.GetAccomodationTypeById(Id.Value);

            model.Id = accomodationType.Id;
            model.Name = accomodationType.Name;
            model.Description = accomodationType.Description;
        }

        return PartialView("_Action", model);
    }

    [HttpPost]
    public ActionResult Action(AccomodationTypesActionModel model)
    {
        var result = false;
        if (model.Id > 0)
        {
            var accomodationType = accomodationTypesService.GetAccomodationTypeById(model.Id);
            accomodationType.Name = model.Name;
            accomodationType.Description = model.Description;

            result = accomodationTypesService.UpdateAccomodationType(accomodationType);
        }
        else
        {
            AccomodationType accomodationType = new()
            {
                Name = model.Name,
                Description = model.Description,
            };

            result = accomodationTypesService.SaveAccomodationType(accomodationType);
        }
        

        object json;
        if (result)
        {
            json = new { Success = true };

        }
        else
        {
            json = new { Success = false, Message = "Unable to perform action on Accomodation Types." };
        }

        return Json(json);

    }

}
