using Library.Domain.DTOs;
namespace Library.Web.Models;
public class HomePageVm
{
    public List<BookDto> Latest { get; set; } = new();
    public List<CategoryDto> Categories { get; set; } = new();
}
