using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication02.Contexts;

namespace WebApplication02.Controllers;

public class PustokController : Controller
{
    PustokDbContext _context {  get; }

    public PustokController(PustokDbContext context)
    {
        this._context = context;
    }

    public async Task<IActionResult> Index()
    {
        var sliders = await _context.Sliders.ToListAsync();
        return View(sliders);
    }
}
