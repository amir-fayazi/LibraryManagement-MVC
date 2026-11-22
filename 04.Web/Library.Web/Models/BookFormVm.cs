using Library.Domain.DTOs;
using System.ComponentModel.DataAnnotations;
namespace Library.Web.Models;
public class BookFormVm
{
    public int Id { get; set; }
    [Required(ErrorMessage = "عنوان کتاب الزامی است.")][StringLength(150)]
    public string Title { get; set; } = "";
    [Required(ErrorMessage = "نام نویسنده الزامی است.")][StringLength(100)]
    public string Author { get; set; } = "";
    [Range(1, int.MaxValue, ErrorMessage = "دسته‌بندی را انتخاب کنید.")]
    public int CategoryId { get; set; }
    [Range(0, 1000000000, ErrorMessage = "قیمت معتبر نیست.")]
    public decimal Price { get; set; }
    [Range(1, 10000, ErrorMessage = "تعداد صفحات باید مثبت باشد.")]
    public int PageCount { get; set; }
    [StringLength(2000)]
    public string Description { get; set; } = "";
    public string? ImagePath { get; set; } = "";
    public IFormFile? CoverFile { get; set; }
    public List<CategoryDto> Categories { get; set; } = new();
}
