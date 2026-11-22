using Microsoft.AspNetCore.Mvc;
namespace Library.Web.Controllers;
public class ReviewsController : BaseController
{
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(int bookId, int rating, string comment)
    {
        if (!UserId.HasValue) return SignInRequired();
        using var app = AppServices.Open();
        Notify(app.Reviews.Create(UserId.Value, bookId, rating, comment));
        return RedirectToAction("Details", "Books", new { id = bookId });
    }
    public IActionResult My()
    {
        if (!UserId.HasValue) return SignInRequired();
        using var app = AppServices.Open();
        return View(app.Reviews.GetByUser(UserId.Value).Data ?? new());
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Edit(int reviewId, int rating, string comment)
    {
        if (!UserId.HasValue) return SignInRequired();
        using var app = AppServices.Open();
        Notify(app.Reviews.Update(UserId.Value, reviewId, rating, comment));
        return RedirectToAction(nameof(My));
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Delete(int reviewId)
    {
        if (!UserId.HasValue) return SignInRequired();
        using var app = AppServices.Open();
        Notify(app.Reviews.Delete(UserId.Value, reviewId));
        return RedirectToAction(nameof(My));
    }
}
