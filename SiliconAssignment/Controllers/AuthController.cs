using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using SiliconAssignment.ViewModels;

namespace SiliconAssignment.Controllers;

public class AuthController(UserService userService) : Controller
{

    private readonly UserService _userService = userService;

    [Route("/signup")]
    [HttpGet]
    public IActionResult SignUp() => View(new SignUpViewModel());


    [HttpPost]
    [Route("/signup")]
    public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
    {
        if (ModelState.IsValid)  
        {
            var result = await _userService.CreateUserAsync(viewModel.Form);
            if (result.StatusCode == Infrastructure.Models.StatusCode.OK)
                return RedirectToAction("SignIn", "Auth");
        }

        return View(viewModel);
    }


    [Route("/signin")]
    [HttpGet]

    public IActionResult SignIn() => View(new SignInViewModel());

    [Route("/signin")]
    [HttpPost]

    public IActionResult SignIn(SignInViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            viewModel.ErrorMessage = "Incorrect email or password";
            return View(viewModel);
        }


        return RedirectToAction("Index", "Home");
    }
}
