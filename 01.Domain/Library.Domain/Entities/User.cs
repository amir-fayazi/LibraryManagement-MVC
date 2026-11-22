using Library.Domain.Enums;
using Library.Domain.Exceptions;
namespace Library.Domain.Entities;
public class User : BaseEntity
{
    public string Username { get; private set; } = "";
    public string PasswordHash { get; private set; } = "";
    public string PasswordSalt { get; private set; } = "";
    public UserRole Role { get; private set; }
    public decimal PenaltyAmount { get; private set; }
    public List<BookLoan> Loans { get; private set; } = new();
    public List<Review> Reviews { get; private set; } = new();
    public List<Wishlist> Wishlists { get; private set; } = new();
    private User() { }
    public User(string username, string passwordHash, string passwordSalt, UserRole role)
    {
        ChangeUsername(username); ChangePassword(passwordHash, passwordSalt); Role = role;
    }
    public void ChangeUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username) || username.Length < 3 || username.Length > 30)
            throw new ValidationException("نام کاربری باید بین ۳ تا ۳۰ کاراکتر باشد.");
        Username = username.Trim();
    }
    public void ChangePassword(string hash, string salt)
    {
        if (string.IsNullOrWhiteSpace(hash) || string.IsNullOrWhiteSpace(salt)) throw new ValidationException("رمز عبور معتبر نیست.");
        PasswordHash = hash; PasswordSalt = salt;
    }
    public void AddPenalty(decimal amount)
    {
        if (amount < 0) throw new ValidationException("مبلغ جریمه معتبر نیست.");
        PenaltyAmount += amount;
    }
}
