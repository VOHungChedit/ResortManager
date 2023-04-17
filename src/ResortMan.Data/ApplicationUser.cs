using Microsoft.AspNetCore.Identity;

namespace ResortMan.Data;

public class ApplicationUser : IdentityUser
{
    public ICollection<ApplicationUserRole> UserRoles { get; set; } = null!;

    public string GetRolesAsString()
    {
        if (UserRoles == null)
            return string.Empty;
        var roles = UserRoles.Select(x => x.Role.Name).ToArray();
        return string.Join(", ", roles);
    }
}
