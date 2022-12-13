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
    private readonly IFileHelperService _fileHelperService;

    public ProfileController(
        UserManager<AppUser> userManager,
        IFileHelperService fileHelperService)
    {
        _userManager = userManager;
        _fileHelperService = fileHelperService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(UserView), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserProfile([FromServices] UserManager<AppUser> userManager)
    {
        var user = await userManager.GetUserAsync(User);
        return Ok(user.Adapt<UserView>());
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUserProfile([FromForm] UpdateUserDto updateUserDto)
    {
        var user = await _userManager.GetUserAsync(User);

        user.FirstName = updateUserDto.FirstName;
        user.LastName = updateUserDto.LastName;
        if (updateUserDto.Avatar is not null)
            user.AvatarUrl = await _fileHelperService.SaveFileAsync(updateUserDto.Avatar, EFileType.Images, EFileFolder.User);

        await _userManager.UpdateAsync(user);
        
        return Ok();
    }
}