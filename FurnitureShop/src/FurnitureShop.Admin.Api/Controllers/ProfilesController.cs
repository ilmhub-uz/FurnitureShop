using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.ViewModel;
using FurnitureShop.Data.Entities;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Admin.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;

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
    public async Task<IActionResult> UpdateProfile([FromBody]UpdateProfileDto updateProfile)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null) return BadRequest();
        user.Email = updateProfile.Email;
        user.FirstName = updateProfile.FirstName;
        user.LastName = updateProfile.LastName;
        user.UserName = updateProfile.UserName;
        await _userManager.UpdateAsync(user);
        return Ok();
    }

    [HttpGet("check")]
    [Authorize]
    public IActionResult IsAuthenticated()
    {
        if(User.Identity is null || User.Identity.IsAuthenticated == false)
            return Unauthorized();
        
        return Ok();
    }
}