using Infrastructure.Models;

namespace SiliconAssignment.ViewModels;

public class SignUpViewModel
{
    public string Title { get; set; } = "Sign Up";

    public SignUpModel Form { get; set; } = new SignUpModel();

    public string? ErrorMessage {  get; set; }

}
