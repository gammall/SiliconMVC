using Infrastructure.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiliconAssignment.ViewModels;

namespace SiliconAssignment.Controllers;

public class AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : Controller
{

    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;

    [Route("/signup")]
    [HttpGet]
    public IActionResult SignUp() {
        if (_signInManager.IsSignedIn(User))
            return RedirectToAction("Account", "Account");

        var signUpViewModel = new SignUpViewModel
        {
            Title = "Sign In"
        };

        return View(signUpViewModel);
    }


    [HttpPost]
    [Route("/signup")]
    public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
    {
        if (ModelState.IsValid)  
        {
            var exists = await _userManager.Users.AnyAsync(x => x.Email == viewModel.Form.Email);
            if(exists)
            {
                ModelState.AddModelError("AlreadyExists", "User with the same email already exists");
                ViewData["ErrorMessage"] = "User with the same email already exists";
                return View(viewModel);
            }
            var userEntity = new UserEntity
            {
                FirstName = viewModel.Form.FirstName,
                LastName = viewModel.Form.LastName,
                Email = viewModel.Form.Email,
                UserName = viewModel.Form.Email
            };
            var result = await _userManager.CreateAsync(userEntity, viewModel.Form.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("SignIn", "Auth");
            }
        }

        return View(viewModel);
    }


    [Route("/signin")]
    [HttpGet]

    public IActionResult SignIn() {
        if (_signInManager.IsSignedIn(User))
            return RedirectToAction("Account", "Account");

        var signInViewModel = new SignInViewModel
        {
            Title = "Sign In"
        };

        return View(signInViewModel);
    }

    [HttpPost]
    [Route("/signin")]

    public async Task<IActionResult> SignIn(SignInViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(viewModel.Form.Email, viewModel.Form.Password, viewModel.Form.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Account", "Account");
            }
        }

        viewModel.ErrorMessage = "Incorrect email or password";
        return View(viewModel);
    }

    [Route("/signout")]
    [HttpGet]
    public new async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
