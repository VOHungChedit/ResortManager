using ResortMan.Entities;

namespace ResortMan.MvcApp.Areas.Dashboard.ViewModels;

public class AccomodationTypesListingModel
{
    public IEnumerable<AccomodationType> AccomodationTypes { get; set; }
}
public class AccomodationTypesActionModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!; //Hotet Room, Apartent
    public string Description { get; set; } = null!;
}
