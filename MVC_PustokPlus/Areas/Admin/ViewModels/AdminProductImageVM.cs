using MVC_PustokPlus.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC_PustokPlus.Areas.Admin.ViewModels;

public class AdminProductImageVM
{
    public int? Id { get; set; }

    public string ImagePath { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public bool IsActive { get; set; } = true;
}
