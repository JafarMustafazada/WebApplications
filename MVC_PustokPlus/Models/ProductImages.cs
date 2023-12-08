
namespace MVC_PustokPlus.Models;

public class ProductImages
{
    public int Id { get; set; }
    public int ProductId { get; set; }

    public string ImagePath { get; set; }
    public bool IsActive { get; set; } = true;

    public Product? Product { get; set; }
}
