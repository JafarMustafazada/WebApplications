using MVC_PustokPlus.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC_PustokPlus.Areas.Admin.ViewModels;

public class AdminCategoryVM
{
    public int? Id { get; set; }

    [MaxLength(16)]
    public string Name { get; set; }
    public bool IsDeleted { get; set; } = false;

    public int? ParentId { get; set; }
    public virtual Category? Parent { get; set; }

    public IEnumerable<Product>? Products { get; set; }
    public virtual ICollection<Category>? Children { get; set; }

}
