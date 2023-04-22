using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResortMan.Entities;
using ResortMan.MvcApp.Areas.Dashboard.ViewModels;
using ResortMan.Services;

namespace ResortMan.MvcApp.Areas.Dashboard.Controllers;

[Area("Dashboard")]
[Authorize(Roles = "Administrator")]
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
	public async Task<IActionResult> ActionAsync(AccomodationPackageActionModel model)
	{
		var result = false;

		var pictures = new List<AccomodationPackagePicture>();
		if (model.Pictures != null)
			foreach (var uploadPicture in model.Pictures)
				using (var memoryStream = new MemoryStream())
				{
					await uploadPicture.CopyToAsync(memoryStream);

					var picture = new AccomodationPackagePicture()
					{
						ContentType = uploadPicture.ContentType,
						Data = memoryStream.ToArray()
					};

					pictures.Add(picture);
				};
		if (model.Id > 0)
		{
			var accomodationPackage = accomodationPackagesService.GetAccomodationPackageById(model.Id);

			accomodationPackage.AccomodationTypeId = model.AccomodationTypeId;
			accomodationPackage.AccomodationType = model.AccomodationType;
			accomodationPackage.Name = model.Name;
			accomodationPackage.NoOfRoom = model.NoOfRoom;
			accomodationPackage.FeePerNight = model.FeePerNight;
			accomodationPackage.Pictures = pictures;
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
				Pictures = pictures,
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
	public ActionResult Delete(int id)
	{
		var accomodationType = accomodationPackagesService.GetAccomodationPackageById(id);

		if (accomodationType == null)
			return NotFound();

		var model = new AccomodationPackageActionModel
		{
			Id = accomodationType.Id
		};

		return PartialView("_Delete", model);
	}

	[HttpDelete]
	[ActionName("Delete")]
	public IActionResult DeleteConfirm(int id)
	{
		var result = false;

		var accomodationPackage = accomodationPackagesService.GetAccomodationPackageById(id);

		if (accomodationPackage == null)
			return BadRequest();
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
