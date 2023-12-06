namespace MVC_PustokPlus.Models;

public class Author
{
    public int Id { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }

    public IEnumerable<Blog>? Blogs { get; set; }
}
