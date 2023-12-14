using MVC_PustokPlus.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVC_PustokPlus.ViewModels;

public class ProductSliderVM
{
    public ProductSliderVM()
    {
        
    }
    public ProductSliderVM(Product product)
    {
        this.Name = product.Name;
        this.Count = product.Count;
        this.Description = product.Description;
        this.Discount = product.Discount;
        this.SellPrice = (product.SellPrice * (100 - (decimal)product.Discount) / 100).ToString("0.00");
        this.Category = product.Category;
        this.CostPrice = product.SellPrice.ToString("0.00");
        this.IsDeleted = product.IsDeleted;
        this.FrontImagePath = product.FrontImagePath;
        this.BackImagePath = product.BackImagePath;
        this.ProductImages = product.ProductImages;
    }

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
    public string BackImagePath { get; set; }

    public Category Category { get; set; }
    public ICollection<ProductImage>? ProductImages { get; set; }
}
