using Microsoft.AspNetCore.Mvc;

namespace SiliconAssignment.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Home";

        return View();
    }
}
