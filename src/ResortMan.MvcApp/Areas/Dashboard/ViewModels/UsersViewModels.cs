using ResortMan.Data;

namespace ResortMan.MvcApp.Areas.Dashboard.ViewModels;

public class UsersListingViewModels
{
	public List<ApplicationUser> Users { get; set; }
	
	public string? SearchTerm { get; set; }
}
