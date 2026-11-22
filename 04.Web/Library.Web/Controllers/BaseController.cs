using Library.Domain.Results;
using Library.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
namespace Library.Web.Controllers;
public abstract class BaseController : Controller
{
    protected int? UserId => HttpContext.Session.GetInt32("UserId");
    protected bool IsAdmin(AppServices app)
    {
        if (!UserId.HasValue) return false;
        var user = app.Users.GetProfile(UserId.Value);
        return user.IsSuccess && user.Data?.Role == UserRole.Admin;
    }
    protected IActionResult SignInRequired() => RedirectToAction("Login", "Account")!;
    protected IActionResult AdminRequired() => UserId.HasValue ? StatusCode(403, "Access denied. Administrator only.") : SignInRequired();
    protected void Notify<T>(ResultDto<T> result)
    {
        TempData[result.IsSuccess ? "Notice" : "Error"] = result.Message;
    }
}
