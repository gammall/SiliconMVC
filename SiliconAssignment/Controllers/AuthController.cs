using Microsoft.AspNetCore.Mvc;

namespace SiliconAssignment.Controllers;

public class AuthController : Controller
{
    [Route("/signup")]
    public IActionResult SignUp()
    {
        return View();
    }
}
