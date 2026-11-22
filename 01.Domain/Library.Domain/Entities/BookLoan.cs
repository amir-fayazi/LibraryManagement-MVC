using Library.Domain.Exceptions;
namespace Library.Domain.Entities;
public class BookLoan : BaseEntity
{
    public int BookId { get; private set; }
    public Book Book { get; private set; } = null!;
    public int UserId { get; private set; }
    public User User { get; private set; } = null!;
    public DateOnly BorrowDate { get; private set; }
    public DateOnly DueDate { get; private set; }
    public DateOnly? ReturnDate { get; private set; }
    public decimal FineAmount { get; private set; }
    public bool IsReturned { get; private set; }
    private BookLoan() { }
    public BookLoan(User user, Book book, DateOnly today)
    {
        User = user ?? throw new ValidationException("کاربر الزامی است.");
        Book = book ?? throw new ValidationException("کتاب الزامی است.");
        UserId = user.Id; BookId = book.Id; BorrowDate = today; DueDate = today.AddDays(7);
    }
    public decimal Return(DateOnly today)
    {
        if (IsReturned) throw new BusinessRuleException("این کتاب قبلاً بازگردانده شده است.");
        if (today < BorrowDate) throw new ValidationException("تاریخ بازگشت معتبر نیست.");
        IsReturned = true; ReturnDate = today;
        FineAmount = Math.Max(0, today.DayNumber - DueDate.DayNumber) * 10000m;
        return FineAmount;
    }
}
