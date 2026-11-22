using Library.Domain.DTOs;
using Library.Domain.Entities;

namespace Library.Domain.Contracts.Repositories;

public interface ILoanRepository
{
    BookLoan? GetById(int id);
    List<LoanDto> GetByUser(int userId);
    List<LoanDto> GetActive();
    bool IsBookOnLoan(int bookId);
    void Add(BookLoan loan);
}
