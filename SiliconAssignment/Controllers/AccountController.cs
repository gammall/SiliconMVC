using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiliconAssignment.ViewModels;

namespace SiliconAssignment.Controllers;

[Authorize]
public class AccountController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, AddressService addressService) : Controller
{

    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly AddressService _addressService = addressService;


    [HttpGet]
    [Route("/account/account")]
    public async Task<IActionResult> Account()
    {
        var AccountInfo = await PopulateAccountInfoAsync();
        var AddressInfo = await PopulateAddressInfoAsync();
        var info = new AccountViewModel
        {
            Account = AccountInfo,
            Address =  AddressInfo
        };
        return View(info);
    }

    // )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))


    [HttpPost]
    [Route("/account/account")]
    public IActionResult Account(AccountViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            
        }

        return View(viewModel);
    }

    // )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))



    private async Task<AccountModel> PopulateAccountInfoAsync()
    {
        var user = await _userManager.GetUserAsync(User);

            var Account = new AccountModel
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                Phone = user.PhoneNumber,
                AddressId = user.AddressId,
            };
        return Account;
    }

    private async Task<AddressModel> PopulateAddressInfoAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return null!;

        
        var response = await _addressService.GetAddressByIdAsync(user.AddressId);
        if (response.Message != "Succeeded") 
            return null!;

        AddressEntity address = (AddressEntity)response.ContentResult;

            var Address = new AddressModel
            {
                Id = user.AddressId,
                StreetName = address.StreetName,
                City = address.City,
                PostalCode = address.PostalCode,
            };
        return Address;
    }
}
    