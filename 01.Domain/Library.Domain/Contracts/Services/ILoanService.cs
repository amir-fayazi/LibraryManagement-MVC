using Library.Domain.DTOs; using Library.Domain.Results;
namespace Library.Domain.Contracts.Services;
public interface ILoanService
{
    ResultDto<int> Borrow(int userId, int bookId);
    ResultDto<decimal> Return(int userId, int loanId);
    ResultDto<List<LoanDto>> GetUserLoans(int userId);
    ResultDto<List<LoanDto>> GetActiveLoans();
}
