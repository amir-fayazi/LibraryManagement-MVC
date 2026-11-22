using Library.Web.Models;
using Microsoft.AspNetCore.Mvc;
namespace Library.Web.Controllers;
public class HomeController : BaseController
{
    public IActionResult Index()
    {
        using var app = AppServices.Open();
        var vm = new HomePageVm
        {
            Latest = app.Books.GetLatest(5).Data ?? new(),
            Categories = app.Categories.GetAll().Data ?? new()
        };
        return View(vm);
    }
    public IActionResult About() => View();
    public IActionResult Contact() => View();
    public IActionResult Error() => View();
}
