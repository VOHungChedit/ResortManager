using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResortMan.MvcApp.Areas.Dashboard.ViewModels;

namespace ResortMan.MvcApp.Areas.Dashboard.Controllers;

[Authorize]
[Area("Dashboard")]
public class RolesController : Controller
{
	private readonly RoleManager<IdentityRole> roleManager;

	public RolesController(RoleManager<IdentityRole> roleManager)
	{
		this.roleManager = roleManager;
	}
	public async Task<IActionResult> IndexAsync()
	{
		var roles = await roleManager.Roles.Select(r => new RoleViewModel()
		{
			Id = r.Id,
			Name = r.Name!
		}).ToListAsync();

		return View(roles);
	}


	[ActionName("Action")]
	public async Task<IActionResult> CreateOrUpdateAsync(string? id)
	{
		if (id == null)
		{
			return PartialView("_Action");
		}

		var role = await roleManager.FindByIdAsync(id);

		if (role == null)
		{
			return NotFound();
		}

		var model = new RoleViewModel
		{
			Id = id,
			Name = role.Name!
		};

		return PartialView("_Action", model);
	}

	[ActionName("Action")]
	[HttpPost]
	public async Task<IActionResult> CreateOrUpdateAsync(string? id, RoleViewModel model)
	{
		IdentityRole? role;
		if (id == null)
		{
			role = new IdentityRole(model.Name);

			var createResult = await roleManager.CreateAsync(role);

			if (createResult.Succeeded)
			{
				return Ok();
			}
			else
			{
				return Problem(createResult.Errors.First().Description);
			}
		}
		else
		{
			role = await roleManager.FindByIdAsync(id);

			if (role == null)
			{
				return NotFound();
			}

			role.Name = model.Name;

			var updateResult = await roleManager.UpdateAsync(role);

			if (updateResult.Succeeded)
			{
				return Ok();
			}
			else
			{
				return Problem(updateResult.Errors.First().Description);
			}
		}
	}

	public async Task<IActionResult> DeleteAsync(string id)
	{
		var role = await roleManager.FindByIdAsync(id);

		if (role == null)
		{
			return NotFound();
		}

		return PartialView("_Delete", new RoleViewModel() { Id = role.Id });
	}

	[ActionName("Delete")]
	[HttpDelete]
	public async Task<IActionResult> DeleteConfirmAsync(string id)
	{
		var role = await roleManager.FindByIdAsync(id);

		if (role == null)
		{
			return Problem("Role dpesnt nit exust");
		}
		var result = await roleManager.DeleteAsync(role);
		if (result.Succeeded)
		{
			return Ok();
		}
		else
		{
			return Problem(result.Errors.First().Description);
		}
	}
}
