using Microsoft.AspNetCore.Mvc;
namespace Library.Web.Controllers;
public class AdminController : BaseController
{
    public IActionResult Index()
    {
        using var app = AppServices.Open();
        if (!IsAdmin(app)) return AdminRequired();
        ViewBag.BookCount = app.Books.GetAll().Data?.Count ?? 0;
        ViewBag.UserCount = app.Users.GetAll().Data?.Count ?? 0;
        ViewBag.ActiveLoans = app.Loans.GetActiveLoans().Data?.Count ?? 0;
        ViewBag.PendingReviews = app.Reviews.GetPending().Data?.Count ?? 0;
        return View();
    }
    public IActionResult ActiveLoans()
    {
        using var app = AppServices.Open();
        if (!IsAdmin(app)) return AdminRequired();
        return View(app.Loans.GetActiveLoans().Data ?? new());
    }
    public IActionResult Users()
    {
        using var app = AppServices.Open();
        if (!IsAdmin(app)) return AdminRequired();
        return View(app.Users.GetAll().Data ?? new());
    }
    public IActionResult PendingReviews()
    {
        using var app = AppServices.Open();
        if (!IsAdmin(app)) return AdminRequired();
        return View(app.Reviews.GetPending().Data ?? new());
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Approve(int id)
    {
        using var app = AppServices.Open();
        if (!IsAdmin(app)) return AdminRequired();
        Notify(app.Reviews.Approve(id)); return RedirectToAction(nameof(PendingReviews));
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Reject(int id)
    {
        using var app = AppServices.Open();
        if (!IsAdmin(app)) return AdminRequired();
        Notify(app.Reviews.Reject(id)); return RedirectToAction(nameof(PendingReviews));
    }
}
