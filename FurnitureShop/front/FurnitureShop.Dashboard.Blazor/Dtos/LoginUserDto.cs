﻿using System.ComponentModel.DataAnnotations;


namespace FurnitureShop.Dashboard.Blazor.Dtos;

public class LoginUserDto
{
    [Required]
    public string? UserName { get; set; }
    [Required]
    public string? Password { get; set; }
}