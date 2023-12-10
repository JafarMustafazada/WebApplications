using Microsoft.EntityFrameworkCore;
using MVC_PustokPlus.Contexts;
using MVC_PustokPlus.Helpers;

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
        });
        //builder.WebHost.UseUrls("http://0.0.0.0:5008");

        var app = builder.Build();
        FileExtension.RootPath = app.Environment.WebRootPath;

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }

        app.UseStaticFiles();

        app.UseRouting();

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