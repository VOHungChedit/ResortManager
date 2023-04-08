using Microsoft.AspNetCore.Mvc;
using ResortMan.Entities;
using ResortMan.MvcApp.Areas.Dashboard.ViewModels;
using ResortMan.Services;

namespace ResortMan.MvcApp.Areas.Dashboard.Controllers;

[Area("Dashboard")]
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
	public IActionResult Listing()
	{
		AccomodationPackagesListingModel model = new AccomodationPackagesListingModel();
		var list = accomodationPackagesService.GetAccomodationPackages();
		return PartialView("_Listing", model);
	}
	public IActionResult Index(string? searchTerm)
	{
		AccomodationPackagesListingModel model;
		if (searchTerm != null)
		{
			model = new AccomodationPackagesListingModel();

			model.AccomodationPackages = accomodationPackagesService.SearchAccomodationPackages(searchTerm);

			return View(model);
		}

		var list = accomodationPackagesService.GetAccomodationPackages();
		model = new AccomodationPackagesListingModel()
		{
			AccomodationPackages = list,
		};
		return View(model);
		//return View();
	}
	[HttpGet]
	public ActionResult Action(int? id)
	{
		var model = new AccomodationPackageActionModel()
		{
			AccomodationTypes = accomodationTypesService.GetAccomodationTypes()
		};

		if (id == null)
		{
			return PartialView("_Action", model);
		}

		var accomodationPackage = accomodationPackagesService.GetAccomodationPackageById(id.Value);
		if (accomodationPackage == null)
			return NotFound();

		model.Id = accomodationPackage.Id;
		model.AccomodationTypeId = accomodationPackage.AccomodationTypeId;
		model.AccomodationType = accomodationPackage.AccomodationType;
		model.Name = accomodationPackage.Name;
		model.NoOfRoom = accomodationPackage.NoOfRoom;
		model.FeePerNight = accomodationPackage.FeePerNight;

		return PartialView("_Action", model);
	}
	[HttpPost]
	public JsonResult Action(AccomodationPackageActionModel model)
	{
		var result = false;
		if (model.Id > 0)
		{
			var accomodationPackage = accomodationPackagesService.GetAccomodationPackageById(model.Id);

			accomodationPackage.AccomodationTypeId = model.AccomodationTypeId;
			accomodationPackage.AccomodationType = model.AccomodationType;
			accomodationPackage.Name = model.Name;
			accomodationPackage.NoOfRoom = model.NoOfRoom;
			accomodationPackage.FeePerNight = model.FeePerNight;

			result = accomodationPackagesService.UpdateAccomodationPackage(accomodationPackage);
		}
		else
		{
			AccomodationPackage accomodationPackage = new()
			{

				AccomodationTypeId = model.AccomodationTypeId,
				Name = model.Name,
				NoOfRoom = model.NoOfRoom,
				FeePerNight = model.FeePerNight,
			};

			result = accomodationPackagesService.UpdateAccomodationPackage(accomodationPackage);
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
	public ActionResult Delete(int Id)
	{
		AccomodationPackageActionModel model = new AccomodationPackageActionModel();

		var accomodationType = accomodationPackagesService.GetAccomodationPackageById(Id);

		model.Id = accomodationType.Id;

		return PartialView("_Delete", model);
	}
	[HttpDelete]
	[ActionName("Delete")]
	public JsonResult DeleteConfirm(int Id)
	{
		var result = false;

		var accomodationPackage = accomodationPackagesService.GetAccomodationPackageById(Id);

		if (accomodationPackage == null)
			result = false;
		else
		{
			result = accomodationPackagesService.DeleteAccomodationPackage(accomodationPackage);
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
