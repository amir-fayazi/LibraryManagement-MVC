using Library.Web.Models;
using Microsoft.AspNetCore.Mvc;
namespace Library.Web.Controllers;
public class BooksController : BaseController
{
    public IActionResult Index(string? search, int? categoryId, bool availableOnly = false)
    {
        using var app = AppServices.Open();
        ViewBag.Search = search;
        ViewBag.CategoryId = categoryId;
        ViewBag.AvailableOnly = availableOnly;
        ViewBag.Categories = app.Categories.GetAll().Data ?? new();
        return View(app.Books.GetAll(search, categoryId, availableOnly).Data ?? new());
    }
    public IActionResult Details(int id)
    {
        using var app = AppServices.Open();
        var result = app.Books.GetDetails(id);
        if (!result.IsSuccess || result.Data is null) return NotFound();
        return View(new BookDetailsVm { Book = result.Data, Reviews = app.Reviews.GetApprovedByBook(id).Data ?? new() });
    }
    [HttpGet]
    public IActionResult Create()
    {
        using var app = AppServices.Open();
        if (!IsAdmin(app)) return AdminRequired();
        return View(new BookFormVm { Categories = app.Categories.GetAll().Data ?? new() });
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(BookFormVm model)
    {
        using var app = AppServices.Open();
        if (!IsAdmin(app)) return AdminRequired();
        if (!ModelState.IsValid) return FormView(model, app);
        var image = Library.Web.Utilities.ImageStorage.Save(model.CoverFile, model.ImagePath, "books", ModelState);
        if (image is null) return FormView(model, app);
        var result = app.Books.Create(model.Title, model.Author, model.CategoryId, model.Price, model.PageCount, model.Description, image);
        if (!result.IsSuccess) { ModelState.AddModelError("", result.Message); return FormView(model, app); }
        Notify(result);
        return RedirectToAction(nameof(Details), new { id = result.Data });
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {
        using var app = AppServices.Open();
        if (!IsAdmin(app)) return AdminRequired();
        var result = app.Books.GetDetails(id);
        if (!result.IsSuccess || result.Data is null) return NotFound();
        var b = result.Data;
        return View("Create", new BookFormVm { Id = b.Id, Title = b.Title, Author = b.Author, CategoryId = b.CategoryId, Price = b.Price, PageCount = b.PageCount, Description = b.Description, ImagePath = b.ImagePath, Categories = app.Categories.GetAll().Data ?? new() });
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Edit(BookFormVm model)
    {
        using var app = AppServices.Open();
        if (!IsAdmin(app)) return AdminRequired();
        if (!ModelState.IsValid) return FormView(model, app);
        var image = Library.Web.Utilities.ImageStorage.Save(model.CoverFile, model.ImagePath, "books", ModelState);
        if (image is null) return FormView(model, app);
        var result = app.Books.Update(model.Id, model.Title, model.Author, model.CategoryId, model.Price, model.PageCount, model.Description, image);
        if (!result.IsSuccess) { ModelState.AddModelError("", result.Message); return FormView(model, app); }
        Notify(result);
        return RedirectToAction(nameof(Details), new { id = model.Id });
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        using var app = AppServices.Open();
        if (!IsAdmin(app)) return AdminRequired();
        var result = app.Books.Delete(id); Notify(result);
        return RedirectToAction(nameof(Index));
    }
    private IActionResult FormView(BookFormVm model, AppServices app)
    {
        model.Categories = app.Categories.GetAll().Data ?? new();
        return View("Create", model);
    }
}
