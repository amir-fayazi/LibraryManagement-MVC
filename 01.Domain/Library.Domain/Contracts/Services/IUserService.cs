using Library.Domain.DTOs; using Library.Domain.Results;
namespace Library.Domain.Contracts.Services;
public interface IUserService
{
    ResultDto<UserDto> GetProfile(int id);
    ResultDto<List<UserDto>> GetAll();
    ResultDto<bool> ChangeUsername(int userId, string newName);
    ResultDto<bool> ChangePassword(int userId, string oldPassword, string newPassword);
}
