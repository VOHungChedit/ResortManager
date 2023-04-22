using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using ResortMan.Entities;
using ResortMan.MvcApp.Areas.Dashboard.ViewModels;
using ResortMan.Services;

namespace ResortMan.MvcApp.Areas.Dashboard.Controllers;

[Area("Dashboard")]
[Authorize(Roles = "Administrator")]
public class AccomodationTypesController : Controller
{
	private readonly AccomodationTypesService accomodationTypesService;

	public AccomodationTypesController(AccomodationTypesService accomodationTypesService)
	{
		this.accomodationTypesService = accomodationTypesService;
	}

	public IActionResult Index(string? searchTerm)
	{
		AccomodationTypesListingModel model;
		if (searchTerm != null)
		{
			model = new AccomodationTypesListingModel();

			model.AccomodationTypes = accomodationTypesService.SearchAccomodationTypes(searchTerm);

			return View(model);
		}

		var list = accomodationTypesService.GetAccomodationTypes();
		model = new AccomodationTypesListingModel()
		{
			AccomodationTypes = list,
		};

		return View(model);
	}

	[HttpGet]
	public ActionResult Action(int? id)
	{
		AccomodationTypesActionModel model = new AccomodationTypesActionModel();

		if (id.HasValue)
		{
			var accomodationType = accomodationTypesService.GetAccomodationTypeById(id.Value);

			model.Id = accomodationType.Id;
			model.Name = accomodationType.Name;
			model.Description = accomodationType.Description;
		}

		return PartialView("_Action", model);
	}

	[HttpPost]
	public JsonResult Action(AccomodationTypesActionModel model)
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

	[HttpGet]
	public ActionResult Delete(int id)
	{
		AccomodationTypesActionModel model = new AccomodationTypesActionModel();

		var accomodationType = accomodationTypesService.GetAccomodationTypeById(id);

		model.Id = accomodationType.Id;

		return PartialView("_Delete", model);
	}

	[HttpDelete]
	[ActionName("Delete")]
	public JsonResult DeleteConfirm(int id)
	{	
		var result = false;
		
		var accomodationType = accomodationTypesService.GetAccomodationTypeById(id);

		result = accomodationTypesService.DeleteAccomodationType(accomodationType);

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
