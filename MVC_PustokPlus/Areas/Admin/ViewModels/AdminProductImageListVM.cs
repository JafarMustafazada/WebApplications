namespace MVC_PustokPlus.Areas.Admin.ViewModels;

public class AdminProductImageListVM
{
    public string ProductName { get; set; }
    public ICollection<string>? ImagePath { get; set; }
}
