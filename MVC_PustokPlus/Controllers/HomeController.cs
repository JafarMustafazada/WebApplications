using Microsoft.AspNetCore.Mvc;
using MVC_PustokPlus.Contexts;
using MVC_PustokPlus.Models;
using MVC_PustokPlus.ViewModels;
using System.Diagnostics;

namespace MVC_PustokPlus.Controllers;

public class HomeController : Controller
{
    Pustoc02DbContext _db { get; }

    public HomeController(Pustoc02DbContext db)
    {
        this._db = db;
    }

    public IActionResult Index()
    {
        return View(_db.Products.Where(p => p.IsDeleted == false).Select(p => new ProductSliderVM
        {
            Name = p.Name,
            Description = p.Description,
            Discount = p.Discount,
            SellPrice = (p.SellPrice * (100 - (decimal)p.Discount) / 100).ToString("0.00"),
            Category = p.Category,
            CostPrice = p.SellPrice.ToString("0.00"),
            Count = p.Count,
            FrontImagePath = p.FrontImagePath,
            BackImagePath = p.BackImagePath,
            ProductImages = p.ProductImages,
        }));
    }
    public IActionResult Detail(int id)
    {

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}
