using Library.Web.Models;
using Microsoft.AspNetCore.Mvc;
namespace Library.Web.Controllers;
public class CategoriesController : BaseController
{
    public IActionResult Index()
    {
        using var app = AppServices.Open();
        return View(app.Categories.GetAll().Data ?? new());
    }
    public IActionResult Create()
    {
        using var app = AppServices.Open();
        if (!IsAdmin(app)) return AdminRequired();
        return View(new CategoryFormVm());
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(CategoryFormVm model)
    {
        using var app = AppServices.Open();
        if (!IsAdmin(app)) return AdminRequired();
        if (!ModelState.IsValid) return View(model);
        var path = Library.Web.Utilities.ImageStorage.Save(model.ImageFile, model.ImagePath, "categories", ModelState);
        if (path is null) return View(model);
        var result = app.Categories.Create(model.Name, model.Description, path);
        if (!result.IsSuccess) { ModelState.AddModelError("", result.Message); return View(model); }
        Notify(result); return RedirectToAction(nameof(Index));
    }
    public IActionResult Edit(int id)
    {
        using var app = AppServices.Open();
        if (!IsAdmin(app)) return AdminRequired();
        var result = app.Categories.GetById(id);
        if (!result.IsSuccess || result.Data is null) return NotFound();
        return View("Create", new CategoryFormVm { Id = result.Data.Id, Name = result.Data.Name, Description = result.Data.Description, ImagePath = result.Data.ImagePath });
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Edit(CategoryFormVm model)
    {
        using var app = AppServices.Open();
        if (!IsAdmin(app)) return AdminRequired();
        if (!ModelState.IsValid) return View("Create", model);
        var path = Library.Web.Utilities.ImageStorage.Save(model.ImageFile, model.ImagePath, "categories", ModelState);
        if (path is null) return View("Create", model);
        var result = app.Categories.Update(model.Id, model.Name, model.Description, path);
        if (!result.IsSuccess) { ModelState.AddModelError("", result.Message); return View("Create", model); }
        Notify(result); return RedirectToAction(nameof(Index));
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        using var app = AppServices.Open();
        if (!IsAdmin(app)) return AdminRequired();
        var result = app.Categories.Delete(id); Notify(result); return RedirectToAction(nameof(Index));
    }
}
