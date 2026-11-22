using Library.Domain.DTOs;
using Library.Domain.Entities;

namespace Library.Domain.Contracts.Repositories;

public interface ICategoryRepository
{
    Category? GetById(int id);
    CategoryDto? GetDetails(int id);
    List<CategoryDto> GetAll();
    bool ExistsByName(string name, int? exceptId = null);
    bool HasBooks(int categoryId);
    void Add(Category category);
    void Delete(Category category);
}
