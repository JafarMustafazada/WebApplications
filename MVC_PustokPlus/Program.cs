using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC_PustokPlus.Contexts;
using MVC_PustokPlus.Helpers;
using MVC_PustokPlus.Models;

namespace MVC_PustokPlus;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Add services to the container.
        builder.Services.AddControllersWithViews();
		builder.Services.AddDbContext<Pustoc02DbContext>(options =>
		{
			options.UseSqlServer(builder.Configuration["ConnectionStrings:MSSql"]);
		}).AddIdentity<AppUser, IdentityRole>(opt =>
		{
			opt.SignIn.RequireConfirmedEmail = false;
			opt.User.RequireUniqueEmail = true;
			opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz0123456789._";
			opt.Lockout.MaxFailedAccessAttempts = 5;
			opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
			opt.Password.RequireNonAlphanumeric = false;
			opt.Password.RequiredLength = 4;
		}).AddDefaultTokenProviders().AddEntityFrameworkStores<Pustoc02DbContext>();
		builder.Services.ConfigureApplicationCookie(options =>
		{
			options.LoginPath = new PathString("/Auth/Login");
			options.LogoutPath = new PathString("/Auth/Logout");
			options.AccessDeniedPath = new PathString("/Home/AccessDenied");

			options.Cookie = new()
			{
				Name = "IdentityCookie",
				HttpOnly = true,
				SameSite = SameSiteMode.Lax,
				SecurePolicy = CookieSecurePolicy.Always
			};
			options.SlidingExpiration = true;
			options.ExpireTimeSpan = TimeSpan.FromDays(30);
		});
		//builder.WebHost.UseUrls("http://0.0.0.0:5008");

		builder.Services.AddSession();

		//builder.Services.AddHttpContextAccessor();
		//builder.Services.AddScoped<LayoutService>();


		var app = builder.Build();
        FileExtension.RootPath = app.Environment.WebRootPath;

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
        );

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}