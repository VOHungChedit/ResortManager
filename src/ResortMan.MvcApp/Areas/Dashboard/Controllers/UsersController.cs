using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResortMan.Data;
using ResortMan.MvcApp.Areas.Dashboard.ViewModels;
using ResortMan.Services;

namespace ResortMan.MvcApp.Areas.Dashboard.Controllers;
[Area("Dashboard")]
public class UsersController : Controller
{
    private readonly ApplicationDbContext context;

    public UsersController(ApplicationDbContext context)
    {
        this.context = context;
    }
    public async Task<IActionResult> Index(string? searchTerm)
    {
        var query = context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .AsQueryable();
        if (searchTerm != null)
        {
            searchTerm = searchTerm.Trim().ToUpperInvariant();
            query = query.Where(u => u.NormalizedUserName!.Contains(searchTerm));
        }

        var viewModel = new UsersListingViewModels()
        {
            SearchTerm = searchTerm,
            Users = await query.ToListAsync()
        };

        return View(viewModel);
    }

    public async Task<IActionResult> RoleAction(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return BadRequest();
        }
        var model = new UserRoleViewModel()
        {
            UserId = id,
            UserRole = await context.UserRoles
                .Include(ur => ur.Role)
                .Where(ur => ur.UserId == id)
                .Select(ur => ur.Role.Name!).FirstOrDefaultAsync(),
            Roles = await context.Roles.Select(r => r.Name!).ToListAsync()
        };

        return PartialView(model);
    }

    [HttpPost]
    public async Task<IActionResult> RoleAction(string id, UserRoleViewModel model)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return Problem("Bad request", statusCode: 404);
        }

        if (ModelState.IsValid)
        {
            var isDelete = model.UserRole == null;

            IdentityRole? role = null;
            if (!isDelete)
            {
                role = await context.Roles.FirstOrDefaultAsync(r => r.Name == model.UserRole);
                if (role == null) return Problem("Role not found", statusCode: 404);
            }

            var userCurrentRole = await context.UserRoles
                .Include(ur => ur.Role)
                .Where(ur => ur.UserId == id)
                .FirstOrDefaultAsync();

            if (userCurrentRole != null)
                context.Remove(userCurrentRole);

            if (!isDelete)
            {
                var userRole = new ApplicationUserRole()
                {
                    RoleId = role!.Id,
                    UserId = id,
                };
                context.Add(userRole);
            }
            await context.SaveChangesAsync();

            return Ok();
        }

        return Problem();
    }

    public async Task<IActionResult> DeleteAsync(string id)
    {
        var user = await context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return PartialView("_Delete", new ApplicationUser() { Id = user.Id });
    }

    [ActionName("Delete")]
    [HttpDelete]
    public async Task<IActionResult> DeleteConfirmAsync(string id)
    {
        var user = await context.Users.FindAsync(id);

        if (user == null)
        {
            return Problem("User doesnt not exust");
        }
        context.Users.Remove(user);
        var isRemoved = await context.SaveChangesAsync() > 0;
        if (isRemoved)
        {
            return Ok();
        }
        else
        {
            return Problem("Cannot delete", statusCode: 500);
        }
    }
}
