using Library.Domain.DTOs; using Library.Domain.Results;
namespace Library.Domain.Contracts.Services;
public interface IAuthService
{
    ResultDto<UserDto> Login(string username, string password);
    ResultDto<UserDto> Register(string username, string password);
}
