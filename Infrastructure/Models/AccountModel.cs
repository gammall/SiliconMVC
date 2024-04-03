﻿namespace Infrastructure.Models;

public class AccountModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; } = null!;
    public string? Biography { get; set; }
}
