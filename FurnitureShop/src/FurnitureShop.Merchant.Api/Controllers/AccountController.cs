using FluentValidation;
using FurnitureShop.Common.Filters;
using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using FurnitureShop.Merchant.Api.Dtos;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Merchant.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ValidateModel]
public class AccountController : ControllerBase
{
    private readonly IValidator<LoginUserDto> _validateLogin;
    private readonly IValidator<RegisterUserDto> _validationRegister;
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(
        AppDbContext context,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IValidator<RegisterUserDto> validation,
        IValidator<LoginUserDto> validateLogin)
    {
        _validateLogin = validateLogin;
        _validationRegister = validation;
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [Authorize(EPermission.CanUpdateProfile)]
    [HttpPut("avatar/filePath")]
    public async Task<IActionResult> UpdateUserProfile([FromBody] UserAvatar avatar)
    {
        var user = await _userManager.GetUserAsync(User);

        user.AvatarUrl = avatar.AvatarUrl;

        await _userManager.UpdateAsync(user);
        
        return Ok();
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] RegisterUserDto dtoModel)
    {
        // var validate = _validationRegister.Validate(dtoModel);
        // if (!validate.IsValid)
        //     return BadRequest("Dto is not valid");

        if(_userManager.Users.Any(u => u.UserName == dtoModel.UserName))
            return BadRequest("This username already exists");

        var user = dtoModel.Adapt<AppUser>();
        var result = await _userManager.CreateAsync(user, dtoModel.Password);

        if (!result.Succeeded)
            return BadRequest();
        await _signInManager.SignInAsync(user, true);
        
        return Ok();
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] LoginUserDto dtoModel)
    {
        var validate = _validateLogin.Validate(dtoModel);
        if (!validate.IsValid)
            return BadRequest("Dto is not valid");

        var result = await _signInManager.PasswordSignInAsync(dtoModel.UserName, dtoModel.Password, true, true);
        if (!result.Succeeded)
            return BadRequest();

        return Ok();
    }
}