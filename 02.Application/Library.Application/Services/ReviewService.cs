using Library.Domain.Contracts.Repositories;
using Library.Domain.Contracts.Services;
using Library.Domain.DTOs;
using Library.Domain.Entities;
using Library.Domain.Exceptions;
using Library.Domain.Results;

namespace Library.Application.Services;

public class ReviewService : ServiceBase, IReviewService
{
    private readonly IReviewRepository _reviews;
    private readonly IUserRepository _users;
    private readonly IBookRepository _books;
    private readonly IUnitOfWork _work;

    public ReviewService(IReviewRepository reviews, IUserRepository users, IBookRepository books, IUnitOfWork work)
    {
        _reviews = reviews;
        _users = users;
        _books = books;
        _work = work;
    }

    private Review Need(int id) => _reviews.GetById(id) ?? throw new NotFoundException("نظر پیدا نشد.");

    public ResultDto<int> Create(int userId, int bookId, int rating, string comment) => Run(() =>
    {
        var user = _users.GetById(userId) ?? throw new NotFoundException("کاربر پیدا نشد.");
        var book = _books.GetById(bookId) ?? throw new NotFoundException("کتاب پیدا نشد.");
        if (_reviews.ExistsByUserAndBook(userId, bookId))
            throw new BusinessRuleException("قبلاً برای این کتاب نظر ثبت کرده‌اید؛ می‌توانید نظر خود را ویرایش کنید.");
        var review = new Review(user, book, rating, comment);
        _reviews.Add(review);
        _work.Save();
        return review.Id;
    }, "نظر ثبت شد و منتظر تأیید است.");

    public ResultDto<bool> Update(int userId, int reviewId, int rating, string comment) => Run(() =>
    {
        var review = Need(reviewId);
        if (review.UserId != userId) throw new BusinessRuleException("فقط نظر خودتان را می‌توانید ویرایش کنید.");
        review.Update(rating, comment);
        _work.Save();
        return true;
    }, "نظر ویرایش شد و منتظر تأیید است.");

    public ResultDto<bool> Delete(int userId, int reviewId) => Run(() =>
    {
        var review = Need(reviewId);
        if (review.UserId != userId) throw new BusinessRuleException("فقط نظر خودتان را می‌توانید حذف کنید.");
        _reviews.Delete(review);
        _work.Save();
        return true;
    }, "نظر حذف شد.");

    public ResultDto<bool> Approve(int reviewId) => Run(() =>
    {
        Need(reviewId).Approve();
        _work.Save();
        return true;
    }, "نظر تأیید شد.");

    public ResultDto<bool> Reject(int reviewId) => Run(() =>
    {
        Need(reviewId).Reject();
        _work.Save();
        return true;
    }, "نظر رد شد.");

    public ResultDto<List<ReviewDto>> GetApprovedByBook(int bookId) =>
        Run(() => _reviews.GetApprovedByBook(bookId));

    public ResultDto<List<ReviewDto>> GetByUser(int userId) => Run(() => _reviews.GetByUser(userId));
    public ResultDto<List<ReviewDto>> GetPending() => Run(() => _reviews.GetPending());
}
