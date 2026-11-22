using Library.Domain.Enums;
using Library.Domain.Exceptions;
namespace Library.Domain.Entities;
public class Review : BaseEntity
{
    public int UserId { get; private set; }
    public User User { get; private set; } = null!;
    public int BookId { get; private set; }
    public Book Book { get; private set; } = null!;
    public int Rating { get; private set; }
    public string Comment { get; private set; } = "";
    public ReviewStatus Status { get; private set; } = ReviewStatus.Pending;
    public DateTime CreatedAt { get; private set; }
    private Review() { }
    public Review(User user, Book book, int rating, string? comment)
    {
        User = user ?? throw new ValidationException("کاربر الزامی است.");
        Book = book ?? throw new ValidationException("کتاب الزامی است.");
        UserId = user.Id; BookId = book.Id; CreatedAt = DateTime.UtcNow;
        Update(rating, comment);
    }
    public void Update(int rating, string? comment)
    {
        if (rating < 1 || rating > 5) throw new ValidationException("امتیاز باید بین ۱ تا ۵ باشد.");
        if ((comment?.Length ?? 0) > 1000) throw new ValidationException("نظر نباید بیشتر از ۱۰۰۰ کاراکتر باشد.");
        Rating = rating; Comment = comment?.Trim() ?? ""; Status = ReviewStatus.Pending;
    }
    public void Approve() { if (Status != ReviewStatus.Pending) throw new BusinessRuleException("فقط نظرات در انتظار بررسی قابل تأیید هستند."); Status = ReviewStatus.Approved; }
    public void Reject() { if (Status != ReviewStatus.Pending) throw new BusinessRuleException("فقط نظرات در انتظار بررسی قابل رد هستند."); Status = ReviewStatus.Rejected; }
}
