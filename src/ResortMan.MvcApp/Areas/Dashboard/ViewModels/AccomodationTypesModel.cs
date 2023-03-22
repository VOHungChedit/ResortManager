using ResortMan.Entities;

namespace ResortMan.MvcApp.Areas.Dashboard.ViewModels;

public class AccomodationTypesListingModel
{
    public IEnumerable<AccomodationType> AccomodationTypes { get; set; }
}
