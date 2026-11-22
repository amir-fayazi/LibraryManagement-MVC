using Library.Domain.Contracts.Repositories;
using Library.Domain.Contracts.Services;
using Library.Domain.DTOs;
using Library.Domain.Entities;
using Library.Domain.Exceptions;
using Library.Domain.Results;

namespace Library.Application.Services;

public class CategoryService : ServiceBase, ICategoryService
{
    private readonly ICategoryRepository _categories;
    private readonly IUnitOfWork _work;

    public CategoryService(ICategoryRepository categories, IUnitOfWork work)
    {
        _categories = categories;
        _work = work;
    }

    private Category Need(int id) => _categories.GetById(id) ?? throw new NotFoundException("دسته‌بندی پیدا نشد.");

    public ResultDto<List<CategoryDto>> GetAll() => Run(() => _categories.GetAll());

    public ResultDto<CategoryDto> GetById(int id) =>
        Run(() => _categories.GetDetails(id) ?? throw new NotFoundException("دسته‌بندی پیدا نشد."));

    public ResultDto<int> Create(string name, string description, string imagePath) => Run(() =>
    {
        if (_categories.ExistsByName(name)) throw new BusinessRuleException("نام دسته‌بندی تکراری است.");
        var category = new Category(name, description, imagePath);
        _categories.Add(category);
        _work.Save();
        return category.Id;
    }, "دسته‌بندی اضافه شد.");

    public ResultDto<bool> Update(int id, string name, string description, string imagePath) => Run(() =>
    {
        if (_categories.ExistsByName(name, id)) throw new BusinessRuleException("نام دسته‌بندی تکراری است.");
        Need(id).Update(name, description, imagePath);
        _work.Save();
        return true;
    }, "دسته‌بندی ویرایش شد.");

    public ResultDto<bool> Delete(int id) => Run(() =>
    {
        var category = Need(id);
        if (_categories.HasBooks(id)) throw new BusinessRuleException("ابتدا کتاب‌های این دسته‌بندی را منتقل کنید.");
        _categories.Delete(category);
        _work.Save();
        return true;
    }, "دسته‌بندی حذف شد.");
}
