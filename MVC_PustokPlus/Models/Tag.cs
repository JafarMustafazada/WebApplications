namespace MVC_PustokPlus.Models;

public class Tag
{
    public int Id { get; set; }

    public string Title { get; set; }

    public IEnumerable<BlogTag>? BlogTags { get; set; }
}
