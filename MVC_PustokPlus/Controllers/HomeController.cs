using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVC_PustokPlus.Contexts;
using MVC_PustokPlus.Models;
using MVC_PustokPlus.ViewModels;
using Newtonsoft.Json;
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
        // most valuable top 5 products
        return View(_db.Products.Where(p => p.IsDeleted == false).OrderByDescending(p => p.SellPrice).Take(5).Select(p => new ProductSliderVM
        {
            Id = p.Id,
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
        //return View();
    }
    public async Task<IActionResult> AddToCart(int? id)
    {
        if (id == null || id <= 0) return BadRequest();
        if (!await _db.Products.AnyAsync(p => p.Id == id)) return NotFound();

        var basket = JsonConvert.DeserializeObject<List<BasketProductVM>>(HttpContext.Request.Cookies["basket"] ?? "[]");
        var existItem = basket.Find(b => b.Id == id);

        if (existItem == null)
        {
            basket.Add(new BasketProductVM
            {
                Id = (int)id,
                Count = 1
            });
        }
        else
        {
            existItem.Count++;
        }

        HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket), new CookieOptions
        {
            MaxAge = TimeSpan.MaxValue
        });

        return Ok();
    }
    public async Task<IActionResult> GetBasket()
    {
        var items = JsonConvert.DeserializeObject<List<BasketProductVM>>(HttpContext.Request.Cookies["basket"] ?? "[]");
        var products = _db.Products.Where(p => items.Select(i => i.Id).Contains(p.Id));
        List<ProductSliderVM> basketItems = new();
        foreach (var item in products)
        {
            ushort count = items.Find(x => x.Id == item.Id).Count;
            decimal price = (item.SellPrice * (100 - (decimal)item.Discount) / 100);

            basketItems.Add(new ProductSliderVM
            {
                Id = item.Id,
                Discount = item.Discount,
                FrontImagePath = item.FrontImagePath,
                Name = item.Name,
                SellPrice = price.ToString("0.00"),
                CostPrice = (price * count).ToString("0.00"),
                Count = count,
            });
        }
        return Json(basketItems);
    }
    public async Task<IActionResult> JsonData(int count = 4, int from = 0)
    {
        var items = _db.Products.Where(p => p.IsDeleted == false).Skip(from).Take(count).Select(p => new ProductSliderVM
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
        });

        return Json(items);
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
