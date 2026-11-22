using Library.Domain.DTOs;
using Library.Domain.Entities;

namespace Library.Domain.Contracts.Repositories;

public interface IUserRepository
{
    User? GetById(int id);
    User? GetByUsername(string username);
    UserDto? GetProfile(int id);
    List<UserDto> GetAll();
    bool ExistsByUsername(string username, int? exceptId = null);
    void Add(User user);
}
