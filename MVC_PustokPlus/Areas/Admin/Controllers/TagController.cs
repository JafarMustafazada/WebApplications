using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_PustokPlus.Areas.Admin.ViewModels;
using MVC_PustokPlus.Models;

namespace MVC_PustokPlus.Areas.Admin.Controllers;

[Area("Admin")]
public class TagController : Controller
{
    // GET: TagBlogController
    public ActionResult Index()
    {
        return View();
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
    public ActionResult Create(TagListVM vm)
    {
        return View(vm);
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
