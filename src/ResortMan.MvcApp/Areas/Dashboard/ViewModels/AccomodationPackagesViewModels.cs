using ResortMan.Entities;
using ResortMan.ViewModels;

namespace ResortMan.MvcApp.Areas.Dashboard.ViewModels;

public class AccomodationPackagesListingModel
{
    public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }
    public int? AccomodationTypeId { get; set; }
    public IEnumerable<AccomodationType> AccomodationTypes { get; set; }
    public string SearchTerm { get; set; }
    //public Pager Pager { get; set; }
}

