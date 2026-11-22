using System.ComponentModel.DataAnnotations;
namespace Library.Web.Models;
public class RegisterVm
{
    [Required][StringLength(30, MinimumLength = 3)]
    public string Username { get; set; } = "";
    [Required][MinLength(6)][DataType(DataType.Password)]
    public string Password { get; set; } = "";
    [Required][Compare(nameof(Password), ErrorMessage = "تکرار رمز عبور مطابقت ندارد.")][DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = "";
}
