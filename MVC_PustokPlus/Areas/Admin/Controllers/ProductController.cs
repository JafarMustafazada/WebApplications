using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_PustokPlus.Areas.Admin.ViewModels;
using MVC_PustokPlus.Contexts;
using MVC_PustokPlus.Helpers;
using MVC_PustokPlus.Models;
using System.IO;

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
        return View(_db.Products.Select(p => new AdminProductVM(p)
        {
            Category = p.Category
        }));
    }
    // GET: ProductController/Create
    public ActionResult Create()
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
    public async Task<ActionResult> Create(AdminProductVM vm)
    {
        if (vm.CostPrice > vm.SellPrice)
        {
            ModelState.AddModelError("CostPrice", "Sell price must be bigger than cost price");
        }
        if (vm.FrontImageFile != null) 
        {
            if (!vm.FrontImageFile.IsCorrectType())
            {
                ModelState.AddModelError("ImageFile", "Wrong file type");
            }
            if (!vm.FrontImageFile.IsValidSize())
            {
                ModelState.AddModelError("ImageFile", "Files length must be less than kb");
            }
        }
        if ((!ModelState.IsValid) || vm.FrontImageFile == null)
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
            CategoryId = vm.CategoryId,
            FrontImagePath = vm.FrontImageFile.SaveAsync("datas").Result,
        };
        await _db.Products.AddAsync(prod);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: ProductController/Edit/5
    public async Task<ActionResult> Update(int id)
    {
        ViewBag.Categories = _db.Categories;
        ViewBag.Id = id;
        Product product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null) return NotFound();

        return View(new AdminProductVM(product)
        {
            Category = product.Category,
        });
    }

    // POST: ProductController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Update(AdminProductVM vm)
    {
        if (vm.CostPrice > vm.SellPrice)
        {
            ModelState.AddModelError("CostPrice", "Sell price must be bigger than cost price");
        }
        if(vm.FrontImageFile != null)
        {
            string filepath = Path.Combine(FileExtension.RootPath, vm.FrontImagePath);
            if (vm.FrontImagePath != null && System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }
            vm.FrontImagePath = await vm.FrontImageFile.SaveAsync("datas");
        }
        if ((!ModelState.IsValid) || vm.Id == null)
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
        Product product = new Product
        {
            Id = (int)vm.Id,
            Name = vm.Name,
            Count = vm.Count,
            Description = vm.Description,
            Discount = vm.Discount,
            CostPrice = vm.CostPrice,
            SellPrice = vm.SellPrice,
            CategoryId = vm.CategoryId
        };

        _db.Products.Update(product);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: ProductController/Delete/5
    public async Task<ActionResult> Delete(int id)
    {
        Product product = await _db.Products.FindAsync(id);
        if (product == null) return NotFound();
        
        product.IsDeleted = true;

        _db.Products.Update(product);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
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
