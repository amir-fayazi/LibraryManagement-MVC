using Library.Web.Models;
using Microsoft.AspNetCore.Mvc;
namespace Library.Web.Controllers;
public class AccountController : BaseController
{
    [HttpGet] public IActionResult Login() => View(new LoginVm());
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Login(LoginVm model)
    {
        if (!ModelState.IsValid) return View(model);
        using var app = AppServices.Open();
        var result = app.Auth.Login(model.Username, model.Password);
        if (!result.IsSuccess || result.Data is null) { ModelState.AddModelError("", result.Message); return View(model); }
        HttpContext.Session.SetInt32("UserId", result.Data.Id);
        HttpContext.Session.SetInt32("IsAdmin", result.Data.Role == Library.Domain.Enums.UserRole.Admin ? 1 : 0);
        TempData["Notice"] = "خوش آمدید، " + result.Data.Username;
        return RedirectToAction("Index", "Home");
    }
    [HttpGet] public IActionResult Register() => View(new RegisterVm());
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Register(RegisterVm model)
    {
        if (!ModelState.IsValid) return View(model);
        using var app = AppServices.Open();
        var result = app.Auth.Register(model.Username, model.Password);
        if (!result.IsSuccess || result.Data is null) { ModelState.AddModelError("", result.Message); return View(model); }
        HttpContext.Session.SetInt32("UserId", result.Data.Id);
        HttpContext.Session.SetInt32("IsAdmin", result.Data.Role == Library.Domain.Enums.UserRole.Admin ? 1 : 0);
        Notify(result);
        return RedirectToAction("Index", "Home");
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Logout() { HttpContext.Session.Clear(); return RedirectToAction("Index", "Home"); }
    [HttpGet]
    public IActionResult Profile()
    {
        if (!UserId.HasValue) return SignInRequired();
        using var app = AppServices.Open();
        var result = app.Users.GetProfile(UserId.Value);
        if (!result.IsSuccess || result.Data is null) return SignInRequired();
        return View(result.Data);
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult ChangeUsername(string username)
    {
        if (!UserId.HasValue) return SignInRequired();
        using var app = AppServices.Open();
        Notify(app.Users.ChangeUsername(UserId.Value, username));
        return RedirectToAction(nameof(Profile));
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult ChangePassword(string currentPassword, string newPassword)
    {
        if (!UserId.HasValue) return SignInRequired();
        using var app = AppServices.Open();
        Notify(app.Users.ChangePassword(UserId.Value, currentPassword, newPassword));
        return RedirectToAction(nameof(Profile));
    }
}
