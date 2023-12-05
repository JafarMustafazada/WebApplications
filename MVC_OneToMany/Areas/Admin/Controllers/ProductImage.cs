using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace MVC_PustokPlusClass.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductImage : Controller
    {
        // GET: ProductImage
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductImage/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductImage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductImage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: ProductImage/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductImage/Edit/5
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

        // GET: ProductImage/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductImage/Delete/5
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
}
