using Library.Domain.Contracts.Repositories;
using Library.Domain.DTOs;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly LibraryDbContext _db;
    public UserRepository(LibraryDbContext db) { _db = db; }

    public User? GetById(int id) => _db.Users.FirstOrDefault(x => x.Id == id);
    public User? GetByUsername(string username) => _db.Users.FirstOrDefault(x => x.Username == username);

    public UserDto? GetProfile(int id) =>
        Project(_db.Users.AsNoTracking().Where(x => x.Id == id)).FirstOrDefault();

    public List<UserDto> GetAll() =>
        Project(_db.Users.AsNoTracking().OrderBy(x => x.Id)).ToList();

    private static IQueryable<UserDto> Project(IQueryable<User> query) =>
        query.Select(x => new UserDto
        {
            Id = x.Id,
            Username = x.Username,
            Role = x.Role,
            PenaltyAmount = x.PenaltyAmount
        });

    public bool ExistsByUsername(string username, int? exceptId = null) =>
        _db.Users.Any(x => x.Username == username && (!exceptId.HasValue || x.Id != exceptId));

    public void Add(User user) => _db.Users.Add(user);
}
