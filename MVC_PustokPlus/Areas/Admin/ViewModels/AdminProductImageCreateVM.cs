using MVC_PustokPlus.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC_PustokPlus.Areas.Admin.ViewModels;

public class AdminProductImageCreateVM
{
    public int ProductId { get; set; }
    public ICollection<IFormFile> Images { get; set; }
}
