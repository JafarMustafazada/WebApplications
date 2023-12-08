using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace MVC_PustokPlus.Models;

public class Category
{
    public int Id { get; set; }
    public int? ParentId { get; set; }

    [MaxLength(16)]
    public string Name { get; set; }
    public bool IsDeleted { get; set; } = false;


    public IEnumerable<Product>? Products { get; set; }
    public virtual Category? Parent { get; set; }
    public virtual ICollection<Category>? Children { get; set; }
}
