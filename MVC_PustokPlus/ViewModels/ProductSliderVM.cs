using MVC_PustokPlus.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVC_PustokPlus.ViewModels;

public class ProductSliderVM
{
    [MaxLength(64)]
    public string Name { get; set; }
    [MaxLength(128)]
    public string Description { get; set; }
    [Column(TypeName = "money")]
    public string SellPrice { get; set; }
    [Column(TypeName = "money")]
    public string CostPrice { get; set; }
    [Range(0, 100)]
    public float Discount { get; set; }
    public ushort Count { get; set; }
    public bool IsDeleted { get; set; } = false;
    public string FrontImagePath { get; set; }

    public Category Category { get; set; }
    public ICollection<ProductImage>? ProductImages { get; set; }
}
