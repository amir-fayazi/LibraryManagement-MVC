using Library.Domain.DTOs;
using Library.Domain.Entities;

namespace Library.Domain.Contracts.Repositories;

public interface IWishlistRepository
{
    Wishlist? GetById(int id);
    List<WishlistDto> GetByUser(int userId);
    bool Exists(int userId, int bookId);
    void Add(Wishlist wishlist);
    void Delete(Wishlist wishlist);
}
