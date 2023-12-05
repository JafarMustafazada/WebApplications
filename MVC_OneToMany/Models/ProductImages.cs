using MVC_OneToMany.Models;

namespace MVC_PustokPlusClass.Models;

public class ProductImages
{
    public int ID { get; set; }

    public string ImagePath { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public bool IsActive { get; set; } = true;

}
