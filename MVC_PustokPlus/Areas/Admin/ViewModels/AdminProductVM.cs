using MVC_PustokPlus.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVC_PustokPlus.Areas.Admin.ViewModels;

public class AdminProductVM
{

    public int? Id { get; set; }

    [MaxLength(64)]
    public string Name { get; set; }
    public string Description { get; set; }
    [Column(TypeName = "money")]
    public decimal SellPrice { get; set; }
    [Column(TypeName = "money")]
    public decimal CostPrice { get; set; }
    [Range(0, 100)]
    public float Discount { get; set; }
    public ushort Count { get; set; }
    public int CategoryId { get; set; }
    public bool IsDeleted { get; set; } = false;
    public string? FrontImagePath { get; set; }
    public string? BackImagePath { get; set; }

    public IFormFile? FrontImageFile { get; set; }
    public IFormFile? BackImageFile { get; set; }
    public Category? Category { get; set; }
    public IEnumerable<ProductImage>? ProductImages { get; set; }
}
