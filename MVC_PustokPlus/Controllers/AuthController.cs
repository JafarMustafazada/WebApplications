using Microsoft.AspNetCore.Mvc;
using MVC_PustokPlus.Models;
using MVC_PustokPlus.ViewModels;
using Microsoft.AspNetCore.Identity;
using MVC_PustokPlus.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
using System.Net;

namespace MVC_PustokPlus.Controllers;

public class AuthController : Controller
{
	SignInManager<AppUser> _signInManager { get; }
	UserManager<AppUser> _userManager { get; }
	RoleManager<IdentityRole> _roleManager { get; }
	EmailService _emailService { get; }

	public AuthController(SignInManager<AppUser> signInManager,
		UserManager<AppUser> userManager,
		RoleManager<IdentityRole> roleManager,
		EmailService emailService)
	{
		this._signInManager = signInManager;
		this._userManager = userManager;
		this._roleManager = roleManager;
		_emailService = emailService;
	}
	public IActionResult Login()
	{
		return View();
	}
	[HttpPost]
	public async Task<IActionResult> Login(string? returnUrl, LoginVM vm)
	{
		AppUser user;
		if (vm.UsernameOrEmail.Contains('@'))
		{
			user = await this._userManager.FindByEmailAsync(vm.UsernameOrEmail);
		}
		else
		{
			user = await this._userManager.FindByNameAsync(vm.UsernameOrEmail);
		}
		if (user == null)
		{
			ModelState.AddModelError("", "Username or password is wrong");
			return View(vm);
		}
		var result = await this._signInManager.PasswordSignInAsync(user, vm.Password, vm.IsRemember, true);
		if (!result.Succeeded)
		{
			if (result.IsLockedOut)
			{
				ModelState.AddModelError("", "Too many attempts wait until " + DateTime.Parse(user.LockoutEnd.ToString()).ToString("HH:mm"));
			}
			else
			{
				ModelState.AddModelError("", "Username or password is wrong");
			}
			return View(vm);
		}
		if (returnUrl != null)
		{
			return LocalRedirect(returnUrl);
		}
		return RedirectToAction("Index", "Home");
	}
	public IActionResult Register()
	{
		return View();
	}
	[HttpPost]
	public async Task<IActionResult> Register(RegisterVM vm)
	{
		if (!ModelState.IsValid)
		{
			return View(vm);
		}
		var user = new AppUser
		{
			Fullname = vm.Fullname,
			Email = vm.Email,
			UserName = vm.Username
		};
		var result = await this._userManager.CreateAsync(user, vm.Password);
		if (!result.Succeeded)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError("", error.Description);
			}
			return View(vm);
		}
		var roleResult = await this._userManager.AddToRoleAsync(user, AuthRoles.Member.ToString());
		if (!roleResult.Succeeded)
		{
			ModelState.AddModelError("", "Something went wrong. Please contact admin");
			return View(vm);
		}

		string body = "";
		using (StreamReader readtext = new StreamReader("template1.html"))
		{
			body = readtext.ReadToEnd();
		}
		this._emailService.Send(user.Email, "Welcome to club buddy", body, true);


		return RedirectToAction(nameof(Login));
	}
	public async Task<IActionResult> Logout()
	{
		await this._signInManager.SignOutAsync();
		return RedirectToAction("Index", "Home");
	}
	public async Task<bool> CreateRoles()
	{
		foreach (var item in Enum.GetValues<AuthRoles>())
		{
			if (!await this._roleManager.RoleExistsAsync(item.ToString()))
			{
				var result = await this._roleManager.CreateAsync(new IdentityRole
				{
					Name = item.ToString()
				});
				if (!result.Succeeded)
				{
					return false;
				}
			}
		}
		return true;
	}
	public async Task UpdateUserIcon()
	{

	}
	[Authorize()]
	public async Task<IActionResult> Profile()
	{
		AppUser user = await this._userManager.FindByNameAsync(User.Identity.Name);
		if (user == null)
		{
			return NotFound();
		}

		return View(new UserVM
		{
			Username = user.UserName,
			Fullname = user.Fullname,
			Email = user.Email,
			ProfileImageUrl = user.ProfileImageUrl,
		});
	}
	[Authorize()]
	[HttpPost]
	public async Task<IActionResult> Profile(UserVM vm)
	{
		AppUser user = await this._userManager.FindByNameAsync(User.Identity.Name);
		if (user == null)
		{
			return NotFound();
		}

		if (vm.ProfileImageFile != null)
		{
			if (!vm.ProfileImageFile.IsCorrectType())
			{
				ModelState.AddModelError("", "Wrong file type");
			}
			if (!vm.ProfileImageFile.IsValidSize(70000))
			{
				ModelState.AddModelError("", "Files length must be less than kb");
			}
			if (ModelState.IsValid)
			{
				string filepath = Path.Combine(FileExtension.RootPath, user.ProfileImageUrl);
				if (System.IO.File.Exists(filepath)) System.IO.File.Delete(filepath);
			}
			user.ProfileImageUrl = await vm.ProfileImageFile.SaveAsync("datas");
		}

		user.Fullname = vm.Fullname;


		if (ModelState.IsValid)
		{
			await this._userManager.UpdateAsync(user);
			return View(vm);
		}

		// will change;
		vm.ProfileImageUrl = user.ProfileImageUrl;
		return View(vm);
	}
}

