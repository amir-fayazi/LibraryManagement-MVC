namespace Library.Domain.DTOs;
public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string ImagePath { get; set; } = "";
    public int BookCount { get; set; }
}
