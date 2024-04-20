using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiliconAssignment.ViewModels;
using System.Net;
using System.Security.Claims;

namespace SiliconAssignment.Controllers;

[Authorize]
public class AccountController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, AddressService addressService, DataContext dataContext) : Controller
{

    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly AddressService _addressService = addressService;
    private readonly DataContext _dataContext = dataContext;

    [HttpGet]
    [Route("/account/account")]
    public async Task<IActionResult> Account()
    {
        var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var user = await _dataContext.Users.Include(i => i.Address).FirstOrDefaultAsync(x => x.Id == nameIdentifier);

        var viewModel = new AccountViewModel
        {
            Account = new AccountModel
            {
                FirstName = user!.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                Phone = user.PhoneNumber,
                Biography = user.Bio
            },
            Address = new AddressModel
            {
                StreetName = user.Address?.StreetName,
                PostalCode = user.Address?.PostalCode,
                City = user.Address?.City
            }
        };


        return View(viewModel);


        //var AccountInfo = await PopulateAccountInfoAsync();
        //var AddressInfo = await PopulateAddressInfoAsync();
        //var info = new AccountViewModel
        //{
        //    Account = AccountInfo,
        //    Address = AddressInfo
        //};
        //return View(info);
    }

    // )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))


    [HttpPost]
    public async Task<IActionResult> UpdateAccount(AccountViewModel viewModel)
    {
        if (TryValidateModel(viewModel.Account!))
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                user.FirstName = viewModel.Account!.FirstName;
                user.LastName = viewModel.Account!.LastName;
                user.Email = viewModel.Account!.Email;
                user.PhoneNumber = viewModel.Account!.Phone;
                user.UserName = viewModel.Account!.Email;
                user.Bio = viewModel.Account!.Biography;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    TempData["StatusMessage"] = "Updated account info successfully";
                }
                else
                {
                    TempData["StatusMessage"] = "Unable to save account info";
                }
            }
        }
        else
        {
            TempData["StatusMessage"] = "Unable to save info";
        }
        return RedirectToAction("account", "account");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateAddress(AccountViewModel viewModel)
    {
        if (TryValidateModel(viewModel.Address!))
        {
            var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var user = await _dataContext.Users.Include(i => i.Address).FirstOrDefaultAsync(x => x.Id == nameIdentifier);
            if (user != null)
            {
                try
                {
                    if (user.Address != null)
                    {
                        user.Address.StreetName = viewModel.Address!.StreetName!;
                        user.Address.PostalCode = viewModel.Address!.PostalCode!;
                        user.Address.City = viewModel.Address!.City!;
                    }

                    else
                    {
                        user.Address = new AddressEntity
                        {
                            StreetName = viewModel.Address!.StreetName!,
                            PostalCode = viewModel.Address!.PostalCode!,
                            City = viewModel.Address!.City!
                        };
                    }

                    _dataContext.SaveChanges();
                    await _dataContext.SaveChangesAsync();
                }
                catch
                {
                    TempData["StatusMessage"] = "Unable to save info";
                }
            }
        }
        else
        {
            TempData["StatusMessage"] = "Unable to save address info";
        }

            return RedirectToAction("account", "account");
    }

    // )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))



    //private async Task<AccountModel> PopulateAccountInfoAsync()
    //{
    //    var user = await _userManager.GetUserAsync(User);

    //    var Account = new AccountModel
    //    {
    //        UserId = user.Id,
    //        FirstName = user.FirstName,
    //        LastName = user.LastName,
    //        Email = user.Email!,
    //        Phone = user.PhoneNumber,
    //        AddressId = user?.AddressId,
    //    };
    //    return Account;
    //}

    //private async Task<AddressModel> PopulateAddressInfoAsync()
    //{
    //    var user = await _userManager.GetUserAsync(User);

    //    if (user == null)
    //        return null!;


    //    var response = await _addressService.GetAddressByIdAsync(user.AddressId);
    //    if (response.Message != "Succeeded")
    //        return new AddressModel();

    //    AddressEntity address = (AddressEntity)response.ContentResult!;
    //    var addressModel = new AddressModel
    //    {
    //        Id = user.AddressId,
    //        StreetName = address.StreetName,
    //        City = address.City,
    //        PostalCode = address.PostalCode,
    //    };
    //    return addressModel;
    //}

    // )))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))))


}
