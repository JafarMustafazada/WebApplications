using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_PustokPlus.Areas.Admin.ViewModels;
using MVC_PustokPlus.Contexts;

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
    public ActionResult Index()
    {
        return View();
    }

    // GET: BlogController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: BlogController/Create
    public ActionResult Create()
    {
        ViewBag.Authors = new SelectList(_db.Authors, "Id", "Name");
        return View();
    }

    // POST: BlogController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(BlogVM vm)
    {

        return View(vm);
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
