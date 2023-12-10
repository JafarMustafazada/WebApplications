namespace MVC_PustokPlus.Areas.Admin.ViewModels;

public class AdminProductImageListVM
{
    public string ProductName { get; set; }
    public class CustomImage
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
    }
    public ICollection<CustomImage>? CustomImages { get; set; }
}
