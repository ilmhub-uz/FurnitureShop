using FurnitureShop.Common.Filters;
using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.Services;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Merchant.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ValidateModel]
public class AccountController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(
        AppDbContext context,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPut("avatar/filePath")]
    public async Task<IActionResult> UpdateUserProfile([FromBody] UserAvatar avatar)
    {
        var user = await _userManager.GetUserAsync(User);

        user.AvatarUrl = avatar.AvatarUrl;

        await _userManager.UpdateAsync(user);
        
        return Ok();
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] RegisterUserDto dtoModel)
    {
        if(_userManager.Users.Any(u => u.UserName == dtoModel.UserName))
            return BadRequest("This username already exists");

        var user = dtoModel.Adapt<AppUser>();
        var result = await _userManager.CreateAsync(user, dtoModel.Password);

        if (!result.Succeeded)
            return BadRequest();

        await _signInManager.SignInAsync(user, true);

        return Ok();
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] LoginUserDto dtoModel)
    {
        var result = await _signInManager.PasswordSignInAsync(dtoModel.UserName, dtoModel.Password, true, true);
        if (!result.Succeeded)
            return BadRequest();

        return Ok();
    }
}