using Microsoft.AspNetCore.Mvc;
using SiliconAssignment.ViewModels;

namespace SiliconAssignment.Controllers;

public class AccountController : Controller
{
    public IActionResult Account()
    {
        return View("~/Views/Auth/Account.cshtml", new AccountViewModel());
    }
}
