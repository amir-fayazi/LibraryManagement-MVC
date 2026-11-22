using Library.Domain.Contracts.Repositories;
using Library.Domain.Contracts.Services;
using Library.Domain.DTOs;
using Library.Domain.Entities;
using Library.Domain.Exceptions;
using Library.Domain.Results;

namespace Library.Application.Services;

public class WishlistService : ServiceBase, IWishlistService
{
    private readonly IWishlistRepository _wishlist;
    private readonly IUserRepository _users;
    private readonly IBookRepository _books;
    private readonly IUnitOfWork _work;

    public WishlistService(IWishlistRepository wishlist, IUserRepository users, IBookRepository books, IUnitOfWork work)
    {
        _wishlist = wishlist;
        _users = users;
        _books = books;
        _work = work;
    }

    public ResultDto<int> Add(int userId, int bookId) => Run(() =>
    {
        var user = _users.GetById(userId) ?? throw new NotFoundException("کاربر پیدا نشد.");
        var book = _books.GetById(bookId) ?? throw new NotFoundException("کتاب پیدا نشد.");
        if (_wishlist.Exists(userId, bookId))
            throw new BusinessRuleException("کتاب قبلاً به علاقه‌مندی‌ها اضافه شده است.");
        var item = new Wishlist(user, book);
        _wishlist.Add(item);
        _work.Save();
        return item.Id;
    }, "کتاب به علاقه‌مندی‌ها اضافه شد.");

    public ResultDto<bool> Remove(int userId, int wishlistId) => Run(() =>
    {
        var item = _wishlist.GetById(wishlistId) ?? throw new NotFoundException("آیتم پیدا نشد.");
        if (item.UserId != userId) throw new BusinessRuleException("فقط علاقه‌مندی‌های خودتان را می‌توانید حذف کنید.");
        _wishlist.Delete(item);
        _work.Save();
        return true;
    }, "آیتم حذف شد.");

    public ResultDto<List<WishlistDto>> GetByUser(int userId) => Run(() => _wishlist.GetByUser(userId));
}
