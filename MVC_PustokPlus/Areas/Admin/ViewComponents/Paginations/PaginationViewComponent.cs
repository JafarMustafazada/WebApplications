using Microsoft.AspNetCore.Mvc;
using MVC_PustokPlus.Areas.Admin.ViewModels;
using MVC_PustokPlus.Contexts;

namespace MVC_PustokPlus.Areas.Admin.ViewComponents.Paginations;

public class PaginationViewComponent : ViewComponent
{
    Pustoc02DbContext _db { get; }
    public PaginationViewComponent(Pustoc02DbContext db)
    {
        this._db = db;
    }

    public async Task<IViewComponentResult> InvokeAsync(string viewcshtml = "Default", PaginationVM pagination = default)
    {
        switch (viewcshtml)
        {
            case "Default":
                return View(viewcshtml, pagination);
            default:
                return View();
        }

    }
}