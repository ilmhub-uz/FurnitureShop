using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.ViewModel;
using FurnitureShop.Data.Entities;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Client.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;


    public ProfileController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }


    [HttpGet]
    [ProducesResponseType(typeof(UserView), StatusCodes.Status200OK)]
    public async Task<IActionResult> UserProfile()
    {
        var user = await _userManager.GetUserAsync(User);
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
