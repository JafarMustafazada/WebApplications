using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_PustokPlus.Areas.Admin.ViewModels;

namespace MVC_PustokPlus.Areas.Admin.Controllers;

[Area("Admin")]
public class BlogController : Controller
{
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
        return View();
    }

    // POST: BlogController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(BlogListVM vm)
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
