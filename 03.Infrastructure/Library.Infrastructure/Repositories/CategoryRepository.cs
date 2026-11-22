using Library.Domain.Contracts.Repositories;
using Library.Domain.DTOs;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly LibraryDbContext _db;
    public CategoryRepository(LibraryDbContext db) { _db = db; }

    public Category? GetById(int id) => _db.Categories.FirstOrDefault(x => x.Id == id);

    public CategoryDto? GetDetails(int id) =>
        Project(_db.Categories.AsNoTracking().Where(x => x.Id == id)).FirstOrDefault();

    public List<CategoryDto> GetAll() =>
        Project(_db.Categories.AsNoTracking().OrderBy(x => x.Id)).ToList();

    private static IQueryable<CategoryDto> Project(IQueryable<Category> query) =>
        query.Select(x => new CategoryDto
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            ImagePath = x.ImagePath,
            BookCount = x.Books.Count()
        });

    public bool ExistsByName(string name, int? exceptId = null) =>
        _db.Categories.Any(x => x.Name == name && (!exceptId.HasValue || x.Id != exceptId));

    public bool HasBooks(int categoryId) => _db.Books.Any(x => x.CategoryId == categoryId);
    public void Add(Category category) => _db.Categories.Add(category);
    public void Delete(Category category) => _db.Categories.Remove(category);
}
