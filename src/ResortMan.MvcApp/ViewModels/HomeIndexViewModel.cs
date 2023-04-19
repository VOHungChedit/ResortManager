
using ResortMan.Entities;

namespace ResortMan.MvcApp.ViewModels;

public class HomeIndexViewModel
{
    public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; } = null!;
}
