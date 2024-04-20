using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class AddressModel
{
    public int? Id { get; set; }


    [Required(ErrorMessage = "A valid address is required")]
    [DataType(DataType.Text)]
    [Display(Name = "Address", Prompt = "Enter your address")]
    public string? StreetName { get; set; }

    [Required(ErrorMessage = "A valid postal code is required")]
    [DataType(DataType.Text)]
    [Display(Name = "Postal Code", Prompt = "Enter your postal code")]
    public string? PostalCode { get; set; }

    [Required(ErrorMessage = "A valid city is required")]
    [DataType(DataType.Text)]
    [Display(Name = "City", Prompt = "Enter your city")]
    public string? City { get; set; }
}
