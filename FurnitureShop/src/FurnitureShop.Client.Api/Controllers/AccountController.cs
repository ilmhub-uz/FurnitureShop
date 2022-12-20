using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.Services.Interfaces;
using FurnitureShop.Common.Filters;
using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Client.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
//[ValidateModel]
public class AccountController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IFileHelperService _fileHelperService;

    public AccountController(
        AppDbContext context,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IFileHelperService fileHelperService)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _fileHelperService = fileHelperService;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromForm] RegisterUserDto dtoModel)
    {
        var user = dtoModel.Adapt<AppUser>();
        //if (dtoModel.Avatar is not null)
        //    user.AvatarUrl = await _fileHelperService.SaveFileAsync(dtoModel.Avatar, EFileType.Images, EFileFolder.User);

        var result = await _userManager.CreateAsync(user, dtoModel.Password);

        if (!result.Succeeded)
            return BadRequest();

        await _signInManager.SignInAsync(user, true);

        return Ok();
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn(LoginUserDto dtoModel)
    {
        var result = await _signInManager.PasswordSignInAsync(dtoModel.UserName, dtoModel.Password, true, true);
        if (!result.Succeeded)
            return BadRequest();

        return Ok();
    }
}