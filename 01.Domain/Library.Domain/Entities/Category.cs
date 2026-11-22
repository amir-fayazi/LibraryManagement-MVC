using Library.Domain.Exceptions;
namespace Library.Domain.Entities;
public class Category : BaseEntity
{
    public string Name { get; private set; } = "";
    public string Description { get; private set; } = "";
    public string ImagePath { get; private set; } = "";
    public List<Book> Books { get; private set; } = new();
    private Category() { }
    public Category(string name, string description, string imagePath) => Update(name, description, imagePath);
    public void Update(string name, string description, string imagePath)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 80) throw new ValidationException("نام دسته‌بندی باید بین ۱ تا ۸۰ کاراکتر باشد.");
        Name = name.Trim(); Description = description?.Trim() ?? ""; ImagePath = imagePath?.Trim() ?? "";
    }
}
