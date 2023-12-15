using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_PustokPlus.Contexts;
using MVC_PustokPlus.ViewModels;

namespace MVC_PustokPlus.ViewComponents.Common;

public class SliderViewComponent : ViewComponent
{
    Pustoc02DbContext _db { get; }
    public SliderViewComponent(Pustoc02DbContext db)
    {
        this._db = db;
    }

    public async Task<IViewComponentResult> InvokeAsync(string viewcshtml = "Default")
    {
        return View(viewcshtml, await _db.Products.Where(p => !p.IsDeleted).
            OrderByDescending(p => p.Id).
            Take(3).Select(p => new ProductSliderVM(p)).
            ToListAsync());
    }
}
