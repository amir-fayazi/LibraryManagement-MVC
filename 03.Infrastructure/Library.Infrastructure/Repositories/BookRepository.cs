using Library.Domain.Contracts.Repositories;
using Library.Domain.DTOs;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _db;
    public BookRepository(LibraryDbContext db) { _db = db; }

    // Tracked entity for update and delete commands.
    public Book? GetById(int id) =>
        _db.Books.FirstOrDefault(x => x.Id == id);

    public BookDto? GetDetails(int id) =>
        Project(_db.Books.AsNoTracking().Where(x => x.Id == id)).FirstOrDefault();

    public List<BookDto> GetAll(string? search = null, int? categoryId = null, bool availableOnly = false)
    {
        IQueryable<Book> query = _db.Books.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(x => x.Title.Contains(term) || x.Author.Contains(term));
        }

        if (categoryId.HasValue)
            query = query.Where(x => x.CategoryId == categoryId.Value);

        if (availableOnly)
            query = query.Where(x => !_db.BookLoans.Any(l => l.BookId == x.Id && !l.IsReturned));

        return Project(query.OrderByDescending(x => x.CreatedAt).ThenByDescending(x => x.Id))
            .ToList();
    }

    public List<BookDto> GetLatest(int count) =>
        Project(_db.Books.AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.Id)
            .Take(count))
        .ToList();

    private IQueryable<BookDto> Project(IQueryable<Book> query) =>
        query.Select(x => new BookDto
        {
            Id = x.Id,
            Title = x.Title,
            Author = x.Author,
            Price = x.Price,
            PageCount = x.PageCount,
            Description = x.Description,
            ImagePath = x.ImagePath,
            CategoryId = x.CategoryId,
            CategoryName = x.Category.Name,
            CreatedAt = x.CreatedAt,
            IsAvailable = !_db.BookLoans.Any(l => l.BookId == x.Id && !l.IsReturned),
            AverageRating = _db.Reviews
                .Where(r => r.BookId == x.Id && r.Status == ReviewStatus.Approved)
                .Average(r => (double?)r.Rating)
        });

    public bool ExistsByTitle(string title, int? exceptId = null) =>
        _db.Books.Any(x => x.Title == title && (!exceptId.HasValue || x.Id != exceptId));

    public bool HasLoanHistory(int bookId) =>
        _db.BookLoans.Any(x => x.BookId == bookId);

    public void Add(Book book) => _db.Books.Add(book);
    public void Delete(Book book) => _db.Books.Remove(book);
}
