using Infrastructure.Models;

namespace SiliconAssignment.ViewModels;

public class AccountViewModel
{
    public string Title { get; set; } = "Account Details";

    public AccountModel Account { get; set; } = new AccountModel();
}
