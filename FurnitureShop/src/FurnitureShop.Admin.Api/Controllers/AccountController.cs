using FurnitureShop.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Admin.Api.Controllers;
[Route("api/accounts")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AccountController : ControllerBase
{

    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(SignInManager<AppUser> signInManager)
    {
        _signInManager = signInManager;
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
