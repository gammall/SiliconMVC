using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiliconAssignment.ViewModels;

namespace SiliconAssignment.Controllers;


public class AccountController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : Controller
{

    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;

    public IActionResult Account()
    {
        return View("~/Views/Auth/Account.cshtml", new AccountViewModel());
    }
}








//[HttpGet]
//[Route("/account/account")]
//public async Task<IActionResult> Account()
//{
//    if (!_signInManager.IsSignedIn(User))
//        return RedirectToAction("SignIn", "Auth");

//    var viewModel = new AccountViewModel
//    {
//        Account = await PopulateBasicInfo()
//    };
//    return View(viewModel);
//}