using MVC_OneToMany.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MVC_PustokPlusClass.Models;

namespace MVC_OneToMany.Areas.Admin.ViewModels
{
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
        public Category? Category { get; set; }
        public bool IsDeleted { get; set; } = false;
        public IEnumerable<ProductImages>? ProductImages { get; set; }
    }
}
