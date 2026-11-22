using Library.Domain.Contracts.Repositories;
using Library.Domain.Contracts.Services;
using Library.Domain.DTOs;
using Library.Domain.Entities;
using Library.Domain.Exceptions;
using Library.Domain.Results;

namespace Library.Application.Services;

public class BookService : ServiceBase, IBookService
{
    private readonly IBookRepository _books;
    private readonly ICategoryRepository _categories;
    private readonly IUnitOfWork _work;

    // Signature kept compatible with AppServices.cs; read-side joins now belong to BookRepository.
    public BookService(IBookRepository books, ICategoryRepository categories,
        ILoanRepository loans, IReviewRepository reviews, IUnitOfWork work)
    {
        _books = books;
        _categories = categories;
        _work = work;
    }

    private Book NeedBook(int id) => _books.GetById(id) ?? throw new NotFoundException("کتاب پیدا نشد.");
    private Category NeedCategory(int id) => _categories.GetById(id) ?? throw new NotFoundException("دسته‌بندی پیدا نشد.");

    public ResultDto<List<BookDto>> GetAll(string? search = null, int? categoryId = null, bool availableOnly = false) =>
        Run(() => _books.GetAll(search, categoryId, availableOnly));

    public ResultDto<List<BookDto>> GetLatest(int count = 5) =>
        Run(() => _books.GetLatest(Math.Clamp(count, 1, 50)));

    public ResultDto<BookDto> GetDetails(int id) =>
        Run(() => _books.GetDetails(id) ?? throw new NotFoundException("کتاب پیدا نشد."));

    public ResultDto<int> Create(string title, string author, int categoryId, decimal price,
        int pages, string description, string imagePath) => Run(() =>
    {
        if (_books.ExistsByTitle(title)) throw new BusinessRuleException("این عنوان قبلاً ثبت شده است.");
        var book = new Book(title, author, NeedCategory(categoryId), price, pages, description, imagePath);
        _books.Add(book);
        _work.Save();
        return book.Id;
    }, "کتاب با موفقیت ثبت شد.");

    public ResultDto<bool> Update(int id, string title, string author, int categoryId, decimal price,
        int pages, string description, string imagePath) => Run(() =>
    {
        var book = NeedBook(id);
        if (_books.ExistsByTitle(title, id)) throw new BusinessRuleException("این عنوان قبلاً ثبت شده است.");
        book.Update(title, author, NeedCategory(categoryId), price, pages, description, imagePath);
        _work.Save();
        return true;
    }, "کتاب ویرایش شد.");

    public ResultDto<bool> Delete(int id) => Run(() =>
    {
        var book = NeedBook(id);
        if (_books.HasLoanHistory(id)) throw new BusinessRuleException("کتابی که سابقه امانت دارد قابل حذف نیست.");
        _books.Delete(book);
        _work.Save();
        return true;
    }, "کتاب حذف شد.");
}
