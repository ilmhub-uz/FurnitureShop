﻿using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Admin.Blazor.Dtos;

public class UpdateUserDto
{
    [Required]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    // public IFormFile? Avatar { get; set; }
}