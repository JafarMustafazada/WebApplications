using System.Collections;

namespace MVC_PustokPlus.Areas.Admin.ViewModels;

public class PaginationVM
{
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public string Controller { get; set; }
    public string Action { get; set; }

    public int? RouteCount { get; set; }
    public bool HasPrev { get; set; }
    public bool HasNext { get; set; }
}
