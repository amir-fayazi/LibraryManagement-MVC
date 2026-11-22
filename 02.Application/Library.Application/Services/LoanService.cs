using Library.Domain.Contracts.Repositories;
using Library.Domain.Contracts.Services;
using Library.Domain.DTOs;
using Library.Domain.Entities;
using Library.Domain.Exceptions;
using Library.Domain.Results;

namespace Library.Application.Services;

public class LoanService : ServiceBase, ILoanService
{
    private readonly ILoanRepository _loans;
    private readonly IBookRepository _books;
    private readonly IUserRepository _users;
    private readonly IUnitOfWork _work;

    public LoanService(ILoanRepository loans, IBookRepository books, IUserRepository users, IUnitOfWork work)
    {
        _loans = loans;
        _books = books;
        _users = users;
        _work = work;
    }

    public ResultDto<int> Borrow(int userId, int bookId) => Run(() =>
    {
        var user = _users.GetById(userId) ?? throw new NotFoundException("کاربر پیدا نشد.");
        var book = _books.GetById(bookId) ?? throw new NotFoundException("کتاب پیدا نشد.");
        if (_loans.IsBookOnLoan(bookId)) throw new BusinessRuleException("این کتاب اکنون در امانت است.");
        var loan = new BookLoan(user, book, DateOnly.FromDateTime(DateTime.UtcNow));
        _loans.Add(loan);
        _work.Save();
        return loan.Id;
    }, "کتاب امانت داده شد.");

    public ResultDto<decimal> Return(int userId, int loanId) => Run(() =>
    {
        var loan = _loans.GetById(loanId) ?? throw new NotFoundException("امانت پیدا نشد.");
        if (loan.UserId != userId) throw new BusinessRuleException("این امانت متعلق به شما نیست.");
        var fine = loan.Return(DateOnly.FromDateTime(DateTime.UtcNow));
        loan.User.AddPenalty(fine);
        _work.Save();
        return fine;
    }, "کتاب بازگردانده شد.");

    public ResultDto<List<LoanDto>> GetUserLoans(int userId) => Run(() => _loans.GetByUser(userId));
    public ResultDto<List<LoanDto>> GetActiveLoans() => Run(() => _loans.GetActive());
}
