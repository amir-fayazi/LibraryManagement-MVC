using Library.Domain.DTOs; using Library.Domain.Results;
namespace Library.Domain.Contracts.Services;
public interface IBookService
{
    ResultDto<List<BookDto>> GetAll(string? search = null, int? categoryId = null, bool availableOnly = false);
    ResultDto<List<BookDto>> GetLatest(int count = 5);
    ResultDto<BookDto> GetDetails(int id);
    ResultDto<int> Create(string title, string author, int categoryId, decimal price, int pages, string description, string? imagePath);
    ResultDto<bool> Update(int id, string title, string author, int categoryId, decimal price, int pages, string description, string imagePath);
    ResultDto<bool> Delete(int id);
}
