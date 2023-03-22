using Microsoft.AspNetCore.Mvc;
using ResortMan.Entities;
using ResortMan.MvcApp.Areas.Dashboard.ViewModels;
using ResortMan.Services;

namespace ResortMan.MvcApp.Areas.Dashboard.Controllers
{
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
    }   
}
