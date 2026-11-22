using Library.Application.Security;
using Library.Domain.Contracts.Repositories;
using Library.Domain.Contracts.Services;
using Library.Domain.DTOs;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Domain.Exceptions;
using Library.Domain.Results;
namespace Library.Application.Services;
public class AuthService : ServiceBase, IAuthService
{
    private readonly IUserRepository _users;
    private readonly IUnitOfWork _work;
    public AuthService(IUserRepository users, IUnitOfWork work) { _users = users; _work = work; }
    public ResultDto<UserDto> Register(string username, string password) => Run(() =>
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 6) throw new ValidationException("رمز عبور باید حداقل ۶ کاراکتر باشد.");
        if (_users.ExistsByUsername(username)) throw new BusinessRuleException("این نام کاربری قبلاً ثبت شده است.");
        var (hash, salt) = PasswordSecurity.Hash(password);
        var user = new User(username, hash, salt, UserRole.Member);
        _users.Add(user); _work.Save();
        return ToDto(user);
    });
    public ResultDto<UserDto> Login(string username, string password) => Run(() =>
    {
        var user = _users.GetByUsername(username);
        if (user is null || !PasswordSecurity.Verify(password, user.PasswordHash, user.PasswordSalt))
            throw new BusinessRuleException("نام کاربری یا رمز عبور اشتباه است.");
        return ToDto(user);
    });
    internal static UserDto ToDto(User user) => new() { Id = user.Id, Username = user.Username, Role = user.Role, PenaltyAmount = user.PenaltyAmount };
}
