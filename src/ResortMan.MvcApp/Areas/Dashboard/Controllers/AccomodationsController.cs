using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResortMan.Entities;
using ResortMan.MvcApp.Areas.Dashboard.ViewModels;
using ResortMan.Services;

namespace ResortMan.MvcApp.Areas.Dashboard.Controllers;

[Area("Dashboard")]
[Authorize(Roles = "Administrator")]
public class AccomodationsController : Controller
{
	private readonly AccomodationPackagesService accomodationPackagesService;
	private readonly AccomodationsService accomodationsService;
	public AccomodationsController(AccomodationsService accomodationsService,
		AccomodationPackagesService accomodationPackagesService)
	{
		this.accomodationsService = accomodationsService;
		this.accomodationPackagesService = accomodationPackagesService;
	}

	public IActionResult Index(string? searchTerm)
	{
		AccomodationsListingModel model;
		if (searchTerm != null)
		{
			model = new AccomodationsListingModel();

			model.Accomodations = accomodationsService.SearchAccomodations(searchTerm);

			return View(model);
		}

		var list = accomodationsService.GetAccomodations();
		model = new AccomodationsListingModel()
		{
			Accomodations = list,
		};
		return View(model);
	}

	[HttpGet]
	public ActionResult Action(int? id)
	{

		var model = new AccomodationActionModel()
		{
			AccomodationPackages = accomodationPackagesService.GetAccomodationPackages()
		};

		if (id == null)
		{
			return PartialView("_Action", model);
		}

		var accomodation = accomodationsService.GetAccomodationById(id.Value);
		if (accomodation == null)
			return NotFound();

		model.Id = accomodation.Id;
		model.AccomodationPackageId = accomodation.AccomodationPackageId;
		model.AccomodationPackage = accomodation.AccomodationPackage;
		model.Name = accomodation.Name;
		model.Description = accomodation.Description;

		return PartialView("_Action", model);
	}

	[HttpPost]
	public IActionResult Action(AccomodationActionModel model)
	{
		var result = false;
		if (model.Id > 0)
		{
			var accomodation = accomodationsService.GetAccomodationById(model.Id);
			if (accomodation == null)
				return NotFound();

			accomodation.AccomodationPackageId = model.AccomodationPackageId;
			accomodation.AccomodationPackage = model.AccomodationPackage;
			accomodation.Name = model.Name;
			accomodation.Description = model.Description;

			result = accomodationsService.UpdateAccomodation(accomodation);
		}
		else
		{
			Accomodation accomodation = new()
			{
				AccomodationPackageId = model.AccomodationPackageId,
				Name = model.Name,
				Description = model.Description,
			};

			result = accomodationsService.UpdateAccomodation(accomodation);
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
		AccomodationActionModel model = new AccomodationActionModel();

		var accomodation = accomodationsService.GetAccomodationById(id);

		if (accomodation == null)
		{
			return NotFound();
		}

		model.Id = accomodation.Id;

		return PartialView("_Delete", model);
	}

	[HttpDelete]
	[ActionName("Delete")]
	public IActionResult DeleteConfirm(int id)
	{
		var accomodation = accomodationsService.GetAccomodationById(id);

		if (accomodation == null)
			return BadRequest();

		var  result = accomodationsService.DeleteAccomodation(accomodation);

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
