using Microsoft.AspNetCore.Mvc;
namespace Library.Web.Controllers;
public class WishlistController : BaseController
{
    public IActionResult Index()
    {
        if (!UserId.HasValue) return SignInRequired();
        using var app = AppServices.Open();
        return View(app.Wishlist.GetByUser(UserId.Value).Data ?? new());
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Add(int bookId)
    {
        if (!UserId.HasValue) return SignInRequired();
        using var app = AppServices.Open();
        Notify(app.Wishlist.Add(UserId.Value, bookId));
        return RedirectToAction("Details", "Books", new { id = bookId });
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Remove(int wishlistId)
    {
        if (!UserId.HasValue) return SignInRequired();
        using var app = AppServices.Open();
        Notify(app.Wishlist.Remove(UserId.Value, wishlistId));
        return RedirectToAction(nameof(Index));
    }
}
