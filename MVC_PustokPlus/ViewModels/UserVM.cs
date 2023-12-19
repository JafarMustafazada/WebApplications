using MVC_PustokPlus.Helpers;
using System.ComponentModel.DataAnnotations;

namespace MVC_PustokPlus.ViewModels;

public class UserVM
{
    public string Username { get; set; }
    [Required(ErrorMessage = "Ad və soy adınızı daxil edin!!!"), MaxLength(36)]
	public string Fullname { get; set; }
	public IFormFile? ProfileImageFile { get; set; }
	public string? ProfileImageUrl { get; set; }
	[Required, DataType(DataType.EmailAddress)]
	public string Email { get; set; }
	[Required(ErrorMessage = "İstifadəçi adınızı daxil edin!!!"), MaxLength(24)]
	public string? Password { get; set; }
	[Required, DataType(DataType.Password), RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{4,}$", ErrorMessage = "Wrong input for password")]
	public string? ConfirmPassword { get; set; }
}
