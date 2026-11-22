using Library.Domain.Enums;
namespace Library.Domain.DTOs;
public class ReviewDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; } = "";
    public int BookId { get; set; }
    public string BookTitle { get; set; } = "";
    public int Rating { get; set; }
    public string Comment { get; set; } = "";
    public ReviewStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
