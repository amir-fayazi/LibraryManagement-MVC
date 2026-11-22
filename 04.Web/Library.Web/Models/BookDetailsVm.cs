using Library.Domain.DTOs;
namespace Library.Web.Models;
public class BookDetailsVm
{
    public BookDto Book { get; set; } = new();
    public List<ReviewDto> Reviews { get; set; } = new();
}
