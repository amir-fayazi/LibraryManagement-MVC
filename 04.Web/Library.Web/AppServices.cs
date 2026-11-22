using Library.Application.Services;
using Library.Domain.Contracts.Services;
using Library.Infrastructure.Data;
using Library.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
namespace Library.Web;
public sealed class AppServices : IDisposable
{
    private static string _connectionString = "";
    public static string WebRootPath { get; private set; } = "";
    public static void Configure(string connectionString, string webRootPath)
    {
        _connectionString = connectionString;
        WebRootPath = webRootPath;
    }
    public static AppServices Open()
    {
        if (string.IsNullOrWhiteSpace(_connectionString)) throw new InvalidOperationException("Database connection string is missing.");
        return new AppServices();
    }
    private readonly LibraryDbContext _db;
    public IAuthService Auth { get; }
    public IBookService Books { get; }
    public ICategoryService Categories { get; }
    public IUserService Users { get; }
    public ILoanService Loans { get; }
    public IReviewService Reviews { get; }
    public IWishlistService Wishlist { get; }
    private AppServices()
    {
        var options = new DbContextOptionsBuilder<LibraryDbContext>().UseSqlServer(_connectionString).Options;
        _db = new LibraryDbContext(options);
        var work = new UnitOfWork(_db);
        var bookRepo = new BookRepository(_db);
        var categoryRepo = new CategoryRepository(_db);
        var userRepo = new UserRepository(_db);
        var loanRepo = new LoanRepository(_db);
        var reviewRepo = new ReviewRepository(_db);
        var wishlistRepo = new WishlistRepository(_db);
        Auth = new AuthService(userRepo, work);
        Books = new BookService(bookRepo, categoryRepo, loanRepo, reviewRepo, work);
        Categories = new CategoryService(categoryRepo, work);
        Users = new UserService(userRepo, work);
        Loans = new LoanService(loanRepo, bookRepo, userRepo, work);
        Reviews = new ReviewService(reviewRepo, userRepo, bookRepo, work);
        Wishlist = new WishlistService(wishlistRepo, userRepo, bookRepo, work);
    }
    public void Dispose() => _db.Dispose();
}
