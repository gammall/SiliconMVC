using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class AddressRepo(DataContext context) : Repo<AddressEntity>(context)
{
    private readonly DataContext _context = context;

    public override Task<ResponseResult> GetAllAsync()
    {
        return base.GetAllAsync();
    }

    public override async Task<ResponseResult> GetOneAsync(Expression<Func<AddressEntity, bool>> predicate)
    {
        try
        {
            var result = await _context.Set<AddressEntity>()
                .FirstOrDefaultAsync(predicate);

            if (result == null)
                return ResponseFactory.NotFound();

            return ResponseFactory.Ok(result);
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }
}

