namespace MVC_PustokPlusClass.Models;

public class Author
{
    public int ID { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }

    public IEnumerable<Blog>? Blogs { get; set; }
}
