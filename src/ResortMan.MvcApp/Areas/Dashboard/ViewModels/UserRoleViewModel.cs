using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ResortMan.Data;

namespace ResortMan.MvcApp.Areas.Dashboard.ViewModels;

public class UserRoleViewModel
{
    public string UserId { get; set; }
    public string? UserRole { get; set; } = string.Empty;

    [ValidateNever]
    public IEnumerable<string> Roles { get; set; }
}
