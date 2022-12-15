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
[Authorize(Roles = "Admin")]
public class ProfilesController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IFileHelperService _fileHelperService;

    public ProfilesController(UserManager<AppUser> userManager, IFileHelperService fileHelperService)
    {
        _userManager = userManager;
        _fileHelperService = fileHelperService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(UserView), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserProfile()
    {
        var user = await _userManager.GetUserAsync(User);
        return Ok(user.Adapt<UserView>());
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status102Processing)]
    public async Task<IActionResult> UpdateUserProfile([FromForm] UpdateUserDto updateUserDto)
    {
        var user = await _userManager.GetUserAsync(User);

        user.UserName = updateUserDto.UserName;
        user.FirstName = updateUserDto.FirstName;
        user.LastName = updateUserDto.LastName;
        if (updateUserDto.Avatar is not null)
            user.AvatarUrl = await _fileHelperService.SaveFileAsync(updateUserDto.Avatar, EFileType.Images, EFileFolder.User);

        await _userManager.UpdateAsync(user);

        return Ok();

    }






}