using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Infrastructure.Services;

public class UserService(UserRepo repo, AddressService addressService)
{
    private readonly UserRepo _repo = repo;
    private readonly AddressService _addressService = addressService;

    public async Task<ResponseResult> CreateUserAsync(SignUpModel model)
    {
        try
        {
            var exists = await _repo.AlreadyExistsAsync(x => x.Email == model.Email);
            if (exists.StatusCode == StatusCode.EXISTS)
                return exists;

            var result = await _repo.CreateOneAsync(UserFactory.Create(model));
            if (result.StatusCode != StatusCode.OK)
                return result;

            return ResponseFactory.Ok("User was created successfully.");
        }

        catch (Exception ex) { return ResponseFactory.Error(ex.Message); }

    }
}