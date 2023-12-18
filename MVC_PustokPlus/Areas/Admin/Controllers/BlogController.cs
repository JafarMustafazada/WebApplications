using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_PustokPlus.Areas.Admin.ViewModels;
using MVC_PustokPlus.Contexts;
using MVC_PustokPlus.Models;

namespace MVC_PustokPlus.Areas.Admin.Controllers;

[Area("Admin")]
public class BlogController : Controller
{
    Pustoc02DbContext _db { get; }

    public BlogController(Pustoc02DbContext db)
    {
        this._db = db;
    }
    // GET: BlogController
    public async Task<ActionResult> Index()
    {
        return View(await _db.Blogs.Select(b => new BlogVM
        {
            Id = b.Id,
            AuthorId = b.Author.Id,
            CreatedAt = b.CreatedAt,
            LastUpdatedAt = b.LastUpdatedAt,
            Description = b.Description,
            Title = b.Title,
            Tags = b.BlogTags.Select(b=>b.Tag)
        }).ToListAsync());
    }

    // GET: BlogController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: BlogController/Create
    public ActionResult Create()
    {
        ViewBag.Authors = new SelectList(_db.Authors.Select(a => new { a.Id, FullName = (a.Name + "_" + a.Surname) }), "Id", "FullName");
        ViewBag.Tags = new SelectList(_db.Tags.Select(t => new TagVM { Id = t.Id, Title = t.Title }), "Id", "Title");
        return View();
    }

    // POST: BlogController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(BlogVM vm)
    {
        var data = _db.Tags.Select(t => new TagVM { Title = t.Title, Id = t.Id});
        foreach (var item in vm.TagsId)
        {
            bool flag = false;
            foreach (var itemVM in data)
            {
                if(itemVM.Id == item) { flag = true; break; }
            }
            if (!flag)
            {
                ModelState.AddModelError("TagsId", "Wrong tag");
                break;
            }
        }
        if (!ModelState.IsValid)
        {
            ViewBag.Authors = new SelectList(_db.Authors.Select(a => new { a.Id, FullName = (a.Name + "_" + a.Surname) }), "Id", "FullName");
            ViewBag.Tags = new SelectList(_db.Tags.Select(t => new TagVM { Id = t.Id, Title = t.Title }), "Id", "Title");
            return View(vm);
        }

        await _db.Blogs.AddAsync(new Blog
        {
            Title = vm.Title,
            Description = vm.Description,
            BlogTags = vm.TagsId.Select(id => new BlogTag
            {
                TagId = id,
            }).ToList(),
        });
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: BlogController/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: BlogController/Edit/5
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

    // GET: BlogController/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: BlogController/Delete/5
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
