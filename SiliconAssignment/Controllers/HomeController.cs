using Microsoft.AspNetCore.Mvc;


namespace SiliconAssignment.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {   
        return View();
    }
}
