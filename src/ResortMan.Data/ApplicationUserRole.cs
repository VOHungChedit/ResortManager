using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortMan.Data;

public class ApplicationUserRole : IdentityUserRole<string>
{
    public ApplicationUser User { get; set; } = null!;
    public IdentityRole Role { get; set; } = null!;
}
