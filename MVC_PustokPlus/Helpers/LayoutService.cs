using MVC_PustokPlus.Contexts;
using MVC_PustokPlus.Models;
using MVC_PustokPlus.Controllers;
using Microsoft.AspNetCore.Identity;

namespace MVC_PustokPlus.Helpers;

public class LayoutService
{
	Pustoc02DbContext _db { get; }
	UserManager<AppUser> _userManager { get; }

	public LayoutService(Pustoc02DbContext db, UserManager<AppUser> userManager)
	{
		_db = db;
		_userManager = userManager;
	}

	public async Task<Setting> GetSettingsAsync(string name = "")
	{
		Setting setting1 = await _db.Settings.FindAsync(1);

		if (name.Length > 0)
		{
			AppUser user = await this._userManager.FindByNameAsync(name);
			if (user?.ProfileImageUrl != null) setting1.AccountIcon = user.ProfileImageUrl;
		}

		return setting1;
	}
}
