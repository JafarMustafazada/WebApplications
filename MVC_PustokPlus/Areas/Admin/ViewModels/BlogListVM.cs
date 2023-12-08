using MVC_PustokPlus.Models;

namespace MVC_PustokPlus.Areas.Admin.ViewModels;

public class BlogListVM
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime LastUpdatedAt { get; set; } = DateTime.Now;

    public IEnumerable<TagListVM>? Tags { get; set; }
}
