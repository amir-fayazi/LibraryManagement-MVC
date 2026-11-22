using System.ComponentModel.DataAnnotations;
namespace Library.Web.Models;
public class CategoryFormVm
{
    public int Id { get; set; }
    [Required(ErrorMessage = "نام دسته‌بندی الزامی است.")][StringLength(80)]
    public string Name { get; set; } = "";
    [StringLength(300)]
    public string Description { get; set; } = "";
    public string ImagePath { get; set; } = "";
    public IFormFile? ImageFile { get; set; }
}
