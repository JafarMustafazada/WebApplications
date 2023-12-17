using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_PustokPlus.Areas.Admin.ViewModels;
using MVC_PustokPlus.Contexts;
using MVC_PustokPlus.Helpers;
using MVC_PustokPlus.Models;
using System.IO;
using System.Xml.Linq;

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
        return View(_db.Products.Select(p => new AdminProductVM()
        {
            Id = p.Id,
            Name = p.Name,
            Count = p.Count,
            Description = p.Description,
            Discount = p.Discount,
            CostPrice = p.CostPrice,
            SellPrice = p.SellPrice,
            IsDeleted = p.IsDeleted,
            CategoryId = p.CategoryId,
            FrontImagePath = p.FrontImagePath,
            BackImagePath = p.BackImagePath,
            ProductImages = p.ProductImages,
            Category = p.Category,
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
        if (!await _db.Categories.AnyAsync(c => c.Id == vm.CategoryId))
        {
            ModelState.AddModelError("CategoryId", "Category doesnt exist");
        }
        if (vm.FrontImageFile != null) 
        {
            if (!vm.FrontImageFile.IsCorrectType())
            {
                ModelState.AddModelError("FrontImageFile", "Wrong file type");
            }
            if (!vm.FrontImageFile.IsValidSize())
            {
                ModelState.AddModelError("FrontImageFile", "Files length must be less than kb");
            }
        }
        else ModelState.AddModelError("FrontImageFile", "You should input file");
        if (vm.BackImageFile != null)
        {
            if (!vm.BackImageFile.IsCorrectType())
            {
                ModelState.AddModelError("BackImageFile", "Wrong file type");
            }
            if (!vm.BackImageFile.IsValidSize())
            {
                ModelState.AddModelError("BackImageFile", "Files length must be less than kb");
            }
        }
        else ModelState.AddModelError("BackImageFile", "You should input file");
        if ((!ModelState.IsValid))
        {
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
            BackImagePath = vm.BackImageFile.SaveAsync("datas").Result
        };
        await _db.Products.AddAsync(prod);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: ProductController/Edit/5
    public async Task<ActionResult> Update(int? id)
    {
        if (id == null || id <= 0) return BadRequest();
        ViewBag.Categories = _db.Categories;
        ViewBag.Id = id;
        Product product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null) return NotFound();

        return View(new AdminProductVM()
        {
            Name = product.Name,
            Count = product.Count,
            Description = product.Description,
            Discount = product.Discount,
            CostPrice = product.CostPrice,
            SellPrice = product.SellPrice,
            IsDeleted = product.IsDeleted,
            CategoryId = product.CategoryId,
            FrontImagePath = product.FrontImagePath,
            BackImagePath = product.BackImagePath,
        });
    }

    // POST: ProductController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Update(int? id, AdminProductVM vm)
    {
        if (id == null || id <= 0) return BadRequest();
        Product product = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (product == null) return NotFound();

        if (vm.CostPrice > vm.SellPrice)
        {
            ModelState.AddModelError("CostPrice", "Sell price must be bigger than cost price");
        }
        if (!await _db.Categories.AnyAsync(c => c.Id == vm.CategoryId))
        {
            ModelState.AddModelError("CategoryId", "Category doesnt exist");
        }
        if (vm.FrontImageFile != null) 
        {
            if (!vm.FrontImageFile.IsCorrectType())
            {
                ModelState.AddModelError("FrontImageFile", "Wrong file type");
            }
            if (!vm.FrontImageFile.IsValidSize(70000))
            {
                ModelState.AddModelError("FrontImageFile", "Files length must be less than kb");
            }
            if (ModelState.IsValid)
            {
                string filepath = Path.Combine(FileExtension.RootPath, product.FrontImagePath);
                if (System.IO.File.Exists(filepath)) System.IO.File.Delete(filepath);
            }
            product.FrontImagePath = await vm.FrontImageFile.SaveAsync("datas");
        }
        if (vm.BackImageFile != null)
        {
            if (!vm.BackImageFile.IsCorrectType())
            {
                ModelState.AddModelError("BackImageFile", "Wrong file type");
            }
            if (!vm.BackImageFile.IsValidSize(70000))
            {
                ModelState.AddModelError("BackImageFile", "Files length must be less than kb");
            }
            if (ModelState.IsValid)
            {
                string filepath = Path.Combine(FileExtension.RootPath, product.BackImagePath);
                if (System.IO.File.Exists(filepath)) System.IO.File.Delete(filepath);
            }
            product.BackImagePath = await vm.BackImageFile.SaveAsync("datas");
        }
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = _db.Categories;
            vm.FrontImagePath = product.FrontImagePath;
            vm.BackImagePath = product.BackImagePath;
            return View(vm);
        }

        product.Name = vm.Name;
        product.Count = vm.Count;
        product.Description = vm.Description;
        product.Discount = vm.Discount;
        product.CostPrice = vm.CostPrice;
        product.SellPrice = vm.SellPrice;
        product.CategoryId = vm.CategoryId;
        product.IsDeleted = vm.IsDeleted;

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
    public async Task<IActionResult> ProductPagination(int page = 1, int count = 8)
    {
        var items = _db.Products.Where(p => !p.IsDeleted).Skip((page - 1) * count).Take(count).Select(p => new AdminProductVM(p));
        int totalCount = await _db.Products.CountAsync(x => !x.IsDeleted);
        //PaginatonVM<IEnumerable<AdminProductListItemVM>> pag = new(totalCount, page, (int)Math.Ceiling((decimal)totalCount / count), items);

        return PartialView("_ProductPaginationPartial", items);
    }
}
