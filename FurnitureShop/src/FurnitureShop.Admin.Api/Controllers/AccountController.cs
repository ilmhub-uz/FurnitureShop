﻿using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Data.Entities;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Admin.Api.Controllers;
[Route("api/accounts")]
[ApiController]
//[Authorize(Roles = "Admin")]
public class AccountController : ControllerBase
{

    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromForm] RegisterUserDto registerUserDto)
    {
        var user = registerUserDto.Adapt<AppUser>();
        var result = await _userManager.CreateAsync(user, registerUserDto.Password);

        if (!result.Succeeded)
            return BadRequest();

        await _signInManager.SignInAsync(user, true);
        return Ok();
    }
    [HttpPost("signin")]
    public async Task<IActionResult> SignIn(string userName, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(userName, password, true, true);
        if (!result.Succeeded)
            return BadRequest();

        return Ok();
    }
}
