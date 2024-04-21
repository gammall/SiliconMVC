using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class SubsribersEntity
{
    [Key]
    public string Email { get; set; } = null!;
}
