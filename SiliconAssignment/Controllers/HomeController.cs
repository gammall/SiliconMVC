using Microsoft.AspNetCore.Mvc;


namespace SiliconAssignment.Controllers;

public class HomeController : Controller
{
    [Route("/")]
    public IActionResult Index() => View();


    [Route("/error")]
    public IActionResult Error404(int statusCode) => View();
}
