using MVC_PustokPlus.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_PustokPlus.Models;

public class Product
{
    public int Id { get; set; }
    public int CategoryId { get; set; }

    [MaxLength(64)]
    public string Name { get; set; }
    [MaxLength(128)]
    public string Description { get; set; }
    [Column(TypeName = "money")]
    public decimal SellPrice { get; set; }
    [Column(TypeName = "money")]
    public decimal CostPrice { get; set; }
    [Range(0,100)]
    public float Discount { get; set; }
    public ushort Count { get; set; }
    public bool IsDeleted { get; set; } = false;
    public string FrontImagePath { get; set; }

    public Category? Category { get; set; }
    public ICollection<ProductImage>? ProductImages { get; set; }
}
