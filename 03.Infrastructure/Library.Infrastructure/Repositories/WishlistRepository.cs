using Library.Domain.Contracts.Repositories;
using Library.Domain.DTOs;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories;

public class WishlistRepository : IWishlistRepository
{
    private readonly LibraryDbContext _db;
    public WishlistRepository(LibraryDbContext db) { _db = db; }

    public Wishlist? GetById(int id) => _db.Wishlists.FirstOrDefault(x => x.Id == id);

    public List<WishlistDto> GetByUser(int userId) =>
        _db.Wishlists.AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new WishlistDto
            {
                Id = x.Id,
                BookId = x.BookId,
                BookTitle = x.Book.Title,
                BookImagePath = x.Book.ImagePath,
                CategoryName = x.Book.Category.Name
            })
            .ToList();

    public bool Exists(int userId, int bookId) =>
        _db.Wishlists.Any(x => x.UserId == userId && x.BookId == bookId);

    public void Add(Wishlist wishlist) => _db.Wishlists.Add(wishlist);
    public void Delete(Wishlist wishlist) => _db.Wishlists.Remove(wishlist);
}
