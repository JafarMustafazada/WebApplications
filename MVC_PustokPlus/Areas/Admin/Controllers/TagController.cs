using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_PustokPlus.Areas.Admin.ViewModels;
using MVC_PustokPlus.Contexts;
using MVC_PustokPlus.Models;

namespace MVC_PustokPlus.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "SuperAdmin, Admin, Moderator")]
public class TagController : Controller
{
    Pustoc02DbContext _db { get; }

    public TagController(Pustoc02DbContext db)
    {
        this._db = db;
    }
    // GET: TagBlogController
    public async Task<ActionResult> Index()
    {
		return View(await _db.Tags.Select(t => new Tag { Id = t.Id, Title = t.Title }).ToListAsync());
		//return View();
    }

    // GET: TagBlogController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }


    public ActionResult Create()
    {
        return View();
    }
    // POST: TagBlogController/CreateTag
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(TagVM vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
		await _db.Tags.AddAsync(new Tag { Title = vm.Title });
		await _db.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
    }


    // GET: TagBlogController/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: TagBlogController/Edit/5
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

    // GET: TagBlogController/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: TagBlogController/Delete/5
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
