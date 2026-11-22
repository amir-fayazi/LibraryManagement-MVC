using Library.Domain.DTOs; using Library.Domain.Results;
namespace Library.Domain.Contracts.Services;
public interface ICategoryService
{
    ResultDto<List<CategoryDto>> GetAll();
    ResultDto<CategoryDto> GetById(int id);
    ResultDto<int> Create(string name, string description, string imagePath);
    ResultDto<bool> Update(int id, string name, string description, string imagePath);
    ResultDto<bool> Delete(int id);
}
