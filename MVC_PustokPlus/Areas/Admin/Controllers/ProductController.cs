using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_PustokPlus.Areas.Admin.ViewModels;
using MVC_PustokPlus.Contexts;
using MVC_PustokPlus.Models;

namespace MVC_PustokPlus.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    Pustoc02DbContext _db { get; }

    public ProductController(Pustoc02DbContext db)  
    {
        this._db = db;
    }

    // GET: ProductController
    public IActionResult Index()
    {

        return View(_db.Products.Select(p => new AdminProductVM
        {
            Id = p.Id,
            Name = p.Name,
            CostPrice = p.CostPrice,
            Discount = p.Discount,
            Category = p.Category,
            IsDeleted = p.IsDeleted,
            Count = p.Count,
            SellPrice = p.SellPrice
        }));
    }
    // GET: ProductController/Create
    public IActionResult Create()
    {
        ViewBag.Categories = _db.Categories;
        return View();
    }

    // GET: ProductController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }


    // POST: ProductController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Create(AdminProductVM vm)
    {
        if (vm.CostPrice > vm.SellPrice)
        {
            ModelState.AddModelError("CostPrice", "Sell price must be bigger than cost price");
        }
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = _db.Categories;
            return View(vm);
        }
        if (!await _db.Categories.AnyAsync(c => c.Id == vm.CategoryId))
        {
            ModelState.AddModelError("CategoryId", "Category doesnt exist");
            ViewBag.Categories = _db.Categories;
            return View(vm);
        }
        Product prod = new Product
        {
            Name = vm.Name,
            Count = vm.Count,
            Description = vm.Description,
            Discount = vm.Discount,
            CostPrice = vm.CostPrice,
            SellPrice = vm.SellPrice,
            CategoryId = vm.CategoryId
        };
        await _db.Products.AddAsync(prod);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: ProductController/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: ProductController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: ProductController/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: ProductController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
