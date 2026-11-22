using Library.Domain.Contracts.Repositories;
namespace Library.Infrastructure.Data;
public class UnitOfWork : IUnitOfWork
{
    private readonly LibraryDbContext _db;
    public UnitOfWork(LibraryDbContext db) { _db = db; }
    public void Save() => _db.SaveChanges();
}
