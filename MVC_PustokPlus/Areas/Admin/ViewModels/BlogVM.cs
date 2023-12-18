using MVC_PustokPlus.Models;

namespace MVC_PustokPlus.Areas.Admin.ViewModels;

public class BlogVM
{
    public int AuthorId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime LastUpdatedAt { get; set; } = DateTime.Now;

    public int Id { get; set; }
    public string? AuthorFull { get; set; }
    public ICollection<int>? TagsId { get; set; }
    public IEnumerable<Tag>? Tags { get; set; }
}
