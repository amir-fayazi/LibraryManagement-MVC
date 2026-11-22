using Library.Application.Security;
using Library.Domain.Contracts.Repositories;
using Library.Domain.Contracts.Services;
using Library.Domain.DTOs;
using Library.Domain.Exceptions;
using Library.Domain.Results;

namespace Library.Application.Services;

public class UserService : ServiceBase, IUserService
{
    private readonly IUserRepository _users;
    private readonly IUnitOfWork _work;

    public UserService(IUserRepository users, IUnitOfWork work)
    {
        _users = users;
        _work = work;
    }

    private Library.Domain.Entities.User Need(int id) =>
        _users.GetById(id) ?? throw new NotFoundException("کاربر پیدا نشد.");

    public ResultDto<UserDto> GetProfile(int id) =>
        Run(() => _users.GetProfile(id) ?? throw new NotFoundException("کاربر پیدا نشد."));

    public ResultDto<List<UserDto>> GetAll() => Run(() => _users.GetAll());

    public ResultDto<bool> ChangeUsername(int userId, string newName) => Run(() =>
    {
        if (_users.ExistsByUsername(newName, userId)) throw new BusinessRuleException("نام کاربری تکراری است.");
        Need(userId).ChangeUsername(newName);
        _work.Save();
        return true;
    }, "نام کاربری تغییر کرد.");

    public ResultDto<bool> ChangePassword(int userId, string oldPassword, string newPassword) => Run(() =>
    {
        var user = Need(userId);
        if (!PasswordSecurity.Verify(oldPassword, user.PasswordHash, user.PasswordSalt))
            throw new BusinessRuleException("رمز فعلی اشتباه است.");
        if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 6)
            throw new ValidationException("رمز جدید باید حداقل ۶ کاراکتر باشد.");
        var (hash, salt) = PasswordSecurity.Hash(newPassword);
        user.ChangePassword(hash, salt);
        _work.Save();
        return true;
    }, "رمز عبور تغییر کرد.");
}
