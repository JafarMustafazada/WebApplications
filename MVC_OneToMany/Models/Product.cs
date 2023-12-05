using MVC_PustokPlusClass.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_OneToMany.Models;

public class Product
{
    public int ID { get; set; }

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

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public IEnumerable<ProductImages>? ProductImages { get; set; }
}
