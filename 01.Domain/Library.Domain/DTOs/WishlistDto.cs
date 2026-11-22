namespace Library.Domain.DTOs;
public class WishlistDto
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public string BookTitle { get; set; } = "";
    public string BookImagePath { get; set; } = "";
    public string CategoryName { get; set; } = "";
}
