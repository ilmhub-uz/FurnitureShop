using FurnitureShop.Common.Filters;
using FurnitureShop.Data.Entities;
using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.Services;
using FurnitureShop.Merchant.Api.ViewModel;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Merchant.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
[ValidateModel]
public class ProfileController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;

    public ProfileController(
        UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    [ProducesResponseType(typeof(UserView), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserProfile([FromServices] UserManager<AppUser> userManager)
    {
        var user = await userManager.GetUserAsync(User);
        return Ok(user.Adapt<UserView>());
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserDto updateUserDto)
    {
        var user = await _userManager.GetUserAsync(User);

        user.FirstName = updateUserDto.FirstName;
        user.LastName = updateUserDto.LastName;
        user.AvatarUrl = updateUserDto.Avatar;
        await _userManager.UpdateAsync(user);
        
        return Ok();
    }
}