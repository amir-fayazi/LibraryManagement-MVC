using Library.Domain.Contracts.Repositories;
using Library.Domain.DTOs;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories;

public class LoanRepository : ILoanRepository
{
    private readonly LibraryDbContext _db;
    public LoanRepository(LibraryDbContext db) { _db = db; }

    // The User entity must be tracked so a return can also update its penalty.
    public BookLoan? GetById(int id) =>
        _db.BookLoans.Include(x => x.User).FirstOrDefault(x => x.Id == id);

    public List<LoanDto> GetByUser(int userId) =>
        Project(_db.BookLoans.AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.BorrowDate))
        .ToList();

    public List<LoanDto> GetActive() =>
        Project(_db.BookLoans.AsNoTracking()
            .Where(x => !x.IsReturned)
            .OrderBy(x => x.DueDate))
        .ToList();

    private static IQueryable<LoanDto> Project(IQueryable<BookLoan> query) =>
        query.Select(x => new LoanDto
        {
            Id = x.Id,
            UserId = x.UserId,
            Username = x.User.Username,
            BookId = x.BookId,
            BookTitle = x.Book.Title,
            BorrowDate = x.BorrowDate,
            DueDate = x.DueDate,
            ReturnDate = x.ReturnDate,
            IsReturned = x.IsReturned,
            FineAmount = x.FineAmount
        });

    public bool IsBookOnLoan(int bookId) =>
        _db.BookLoans.Any(x => x.BookId == bookId && !x.IsReturned);

    public void Add(BookLoan loan) => _db.BookLoans.Add(loan);
}
