using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Admin.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly SignInManager<AppUser> _signInManager;
    public AccountsController(SignInManager<AppUser> signInManager)
    {
        _signInManager = signInManager;
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody]LoginUserDto userDto)
    {
        var result = await _signInManager.PasswordSignInAsync(userDto.UserName, userDto.Password, true, true);
        if (!result.Succeeded)
            return BadRequest();

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }
}
