﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        return View();
        //return View(_db.Products.Where(p => p.IsDeleted == false).Select(p => new ProductSliderVM
        //{
        //    Name = p.Name,
        //    Description = p.Description,
        //    Discount = p.Discount,
        //    SellPrice = (p.SellPrice * (100 - (decimal)p.Discount) / 100).ToString("0.00"),
        //    Category = p.Category,
        //    CostPrice = p.SellPrice.ToString("0.00"),
        //    Count = p.Count,
        //    FrontImagePath = p.FrontImagePath,
        //    BackImagePath = p.BackImagePath,
        //    ProductImages = p.ProductImages,
        //}));
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
