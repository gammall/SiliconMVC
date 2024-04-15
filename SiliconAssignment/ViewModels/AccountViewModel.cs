using Infrastructure.Models;

namespace SiliconAssignment.ViewModels;


public class AccountViewModel
{
    public AccountModel Account { get; set; } = null!;
    public AddressModel Address { get; set; } = null!;
}