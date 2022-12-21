using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.ViewModel;
using FurnitureShop.Data.Entities;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Admin.Api.Controllers;

[Route("api/profiles")]
[ApiController]
public class ProfilesController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager ;

    public ProfilesController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    [ProducesResponseType(typeof(UserView), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserProfile()
    {
        var user = await _userManager.GetUserAsync(User);
        return Ok(user.Adapt<UserView>());
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProfile(UpdateProfileDto updateProfile)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null) return BadRequest();
        user.Email = updateProfile.Email;
        user.FirstName = updateProfile.FirstName;
        user.UserName = updateProfile.UserName;
        await _userManager.UpdateAsync(user);
        return Ok();
    }

}