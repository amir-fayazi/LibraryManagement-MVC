using Library.Domain.Contracts.Repositories;
using Library.Domain.DTOs;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly LibraryDbContext _db;
    public ReviewRepository(LibraryDbContext db) { _db = db; }

    public Review? GetById(int id) => _db.Reviews.FirstOrDefault(x => x.Id == id);

    public List<ReviewDto> GetApprovedByBook(int bookId) =>
        Project(_db.Reviews.AsNoTracking()
            .Where(x => x.BookId == bookId && x.Status == ReviewStatus.Approved)
            .OrderByDescending(x => x.CreatedAt))
        .ToList();

    public List<ReviewDto> GetPending() =>
        Project(_db.Reviews.AsNoTracking()
            .Where(x => x.Status == ReviewStatus.Pending)
            .OrderBy(x => x.CreatedAt))
        .ToList();

    public List<ReviewDto> GetByUser(int userId) =>
        Project(_db.Reviews.AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt))
        .ToList();

    private static IQueryable<ReviewDto> Project(IQueryable<Review> query) =>
        query.Select(x => new ReviewDto
        {
            Id = x.Id,
            UserId = x.UserId,
            Username = x.User.Username,
            BookId = x.BookId,
            BookTitle = x.Book.Title,
            Rating = x.Rating,
            Comment = x.Comment,
            Status = x.Status,
            CreatedAt = x.CreatedAt
        });

    public bool ExistsByUserAndBook(int userId, int bookId) =>
        _db.Reviews.Any(x => x.UserId == userId && x.BookId == bookId);

    public void Add(Review review) => _db.Reviews.Add(review);
    public void Delete(Review review) => _db.Reviews.Remove(review);
}
