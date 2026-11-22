using Library.Domain.Exceptions;
namespace Library.Domain.Entities;
public class Wishlist : BaseEntity
{
    public int UserId { get; private set; }
    public User User { get; private set; } = null!;
    public int BookId { get; private set; }
    public Book Book { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    private Wishlist() { }
    public Wishlist(User user, Book book)
    {
        User = user ?? throw new ValidationException("کاربر الزامی است.");
        Book = book ?? throw new ValidationException("کتاب الزامی است.");
        UserId = user.Id; BookId = book.Id; CreatedAt = DateTime.UtcNow;
    }
}
