using Microsoft.AspNetCore.Mvc;
namespace Library.Web.Controllers;
public class LoansController : BaseController
{
    public IActionResult My()
    {
        if (!UserId.HasValue) return SignInRequired();
        using var app = AppServices.Open();
        return View(app.Loans.GetUserLoans(UserId.Value).Data ?? new());
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Borrow(int bookId)
    {
        if (!UserId.HasValue) return SignInRequired();
        using var app = AppServices.Open();
        Notify(app.Loans.Borrow(UserId.Value, bookId));
        return RedirectToAction("Details", "Books", new { id = bookId });
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Return(int loanId)
    {
        if (!UserId.HasValue) return SignInRequired();
        using var app = AppServices.Open();
        Notify(app.Loans.Return(UserId.Value, loanId));
        return RedirectToAction(nameof(My));
    }
}
