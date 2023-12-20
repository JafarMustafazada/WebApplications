using Microsoft.AspNetCore.Mvc;

namespace MVC_PustokPlus.Controllers;

public class SearchController : Controller
{
    public IActionResult Index(string? q)
    {
        return View();
    }
}
