﻿//using FurnitureShop.Data.Entities;

namespace FurnitureShop.Client.Blazor.Shared;

public class UserView
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    //public EUserStatus Status { get; set; }
    public string? AvatarUrl { get; set; }
}