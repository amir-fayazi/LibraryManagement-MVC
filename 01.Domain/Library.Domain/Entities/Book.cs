using Library.Domain.Exceptions;
namespace Library.Domain.Entities;
public class Book : BaseEntity
{
    public string Title { get; private set; } = "";
    public string Author { get; private set; } = "";
    public decimal Price { get; private set; }
    public int PageCount { get; private set; }
    public string Description { get; private set; } = "";
    public string ImagePath { get; private set; } = "";
    public DateTime CreatedAt { get; private set; }
    public int CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;
    public List<BookLoan> Loans { get; private set; } = new();
    public List<Review> Reviews { get; private set; } = new();
    public List<Wishlist> Wishlists { get; private set; } = new();
    private Book() { }
    public Book(string title, string author, Category category, decimal price, int pageCount, string description, string imagePath)
    {
        CreatedAt = DateTime.UtcNow;
        Update(title, author, category, price, pageCount, description, imagePath);
    }
    public void Update(string title, string author, Category category, decimal price, int pageCount, string description, string imagePath)
    {
        if (string.IsNullOrWhiteSpace(title) || title.Length > 150) throw new ValidationException("عنوان کتاب معتبر نیست.");
        if (string.IsNullOrWhiteSpace(author) || author.Length > 100) throw new ValidationException("نام نویسنده معتبر نیست.");
        if (category is null) throw new ValidationException("دسته‌بندی الزامی است.");
        if (price < 0 || price > 1000000000) throw new ValidationException("قیمت معتبر نیست.");
        if (pageCount < 1 || pageCount > 10000) throw new ValidationException("تعداد صفحات معتبر نیست.");
        if ((description?.Length ?? 0) > 2000) throw new ValidationException("توضیحات بیش از حد طولانی است.");
        Title = title.Trim(); Author = author.Trim(); Category = category; CategoryId = category.Id;
        Price = price; PageCount = pageCount; Description = description?.Trim() ?? ""; ImagePath = imagePath?.Trim() ?? "";
    }
}
