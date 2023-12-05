using MVC_OneToMany.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC_OneToMany.Areas.Admin.ViewModels;

public class AdminCategoryVM
{
    public int? Id { get; set; }

    [MaxLength(16)]
    public string Name { get; set; }
    public IEnumerable<Product>? Products { get; set; }
    public bool IsDeleted { get; set; } = false;

}
