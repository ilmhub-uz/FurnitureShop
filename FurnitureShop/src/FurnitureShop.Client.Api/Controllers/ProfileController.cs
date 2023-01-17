using FluentValidation;
using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.ViewModel;
using FurnitureShop.Common.Filters;
using FurnitureShop.Data.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Client.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfileController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IValidator<UpdateUserDto> _updateuservalidator;

    public ProfileController(UserManager<AppUser> userManager ,
     IValidator<UpdateUserDto> updateuservalidator)
    {
        _updateuservalidator = updateuservalidator;
        _userManager = userManager;
    }


    [HttpGet]
    [ProducesResponseType(typeof(UserView), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
    [Authorize(EPermission.CanReadProfile)]
    public async Task<IActionResult> UserProfile()
    {
        var user = await _userManager.GetUserAsync(User);
        return Ok(user.Adapt<UserView>());
    }


    [HttpPut]
    [Authorize(EPermission.CanUpdateProfile)]
    public async Task<IActionResult> UpdateUserProfile([FromForm] UpdateUserDto updateUserDto)
    {
        var result = _updateuservalidator.Validate(updateUserDto);
        if(!result.IsValid)
        {
            throw new BadHttpRequestException(result.Errors.First().ErrorMessage);
        }
        var user = await _userManager.GetUserAsync(User);

        if (_userManager.Users.Any(u => u.UserName == updateUserDto.UserName))
        {
            return BadRequest("User with the username already exists");
        }

        user.UserName = updateUserDto.UserName;
        user.FirstName = updateUserDto.FirstName;
        user.LastName = updateUserDto.LastName;
        user.AvatarUrl = updateUserDto.Avatar;
        await _userManager.UpdateAsync(user);

        return Ok();
    }

}
