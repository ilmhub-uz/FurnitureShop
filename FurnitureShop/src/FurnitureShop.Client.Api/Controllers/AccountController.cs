using FluentValidation;
using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.Services.Interfaces;
using FurnitureShop.Common.Filters;
using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;

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
    private readonly IValidator<RegisterUserDto> _registeruservalidator;
    private readonly IValidator<LoginUserDto> _loginuservalidator;
    public AccountController(
        IValidator<RegisterUserDto> registeruservalidator,
        AppDbContext context,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IFileHelperService fileHelperService,
        IValidator<LoginUserDto> loginvalidator)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _loginuservalidator= loginvalidator;
        _fileHelperService = fileHelperService;
        _registeruservalidator = registeruservalidator;
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] RegisterUserDto dtoModel)
    {
        var resultvalidator = _registeruservalidator.Validate(dtoModel);
        if (!resultvalidator.IsValid)
        {
            throw new BadHttpRequestException(resultvalidator.Errors.First().ErrorMessage);
        }

        if (_userManager.Users.Any(u => u.UserName == dtoModel.UserName))
        {
            return BadRequest("User with the username already exists");
        }

        var user = dtoModel.Adapt<AppUser>();

        var result = await _userManager.CreateAsync(user, dtoModel.Password);
        if (!result.Succeeded)
            return BadRequest();

        await _signInManager.SignInAsync(user, true);

        return Ok();
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn(LoginUserDto dtoModel)
    {
        var resultvalidator = _loginuservalidator.Validate(dtoModel);
        if (!resultvalidator.IsValid)
        {
            throw new BadHttpRequestException(resultvalidator.Errors.First().ErrorMessage);
        }
        var result = await _signInManager.PasswordSignInAsync(dtoModel.UserName, dtoModel.Password, true, true);
        if (!result.Succeeded)
            return BadRequest();

        return Ok();
    }
}