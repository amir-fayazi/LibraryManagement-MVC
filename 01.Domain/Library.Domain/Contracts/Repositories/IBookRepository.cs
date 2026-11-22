using Library.Domain.DTOs;
using Library.Domain.Entities;

namespace Library.Domain.Contracts.Repositories;

public interface IBookRepository
{
    Book? GetById(int id);
    BookDto? GetDetails(int id);
    List<BookDto> GetAll(string? search = null, int? categoryId = null, bool availableOnly = false);
    List<BookDto> GetLatest(int count);
    bool ExistsByTitle(string title, int? exceptId = null);
    bool HasLoanHistory(int bookId);
    void Add(Book book);
    void Delete(Book book);
}
