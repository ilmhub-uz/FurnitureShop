using FurnitureShop.Common.Exceptions;
using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using FurnitureShop.Files.Api.Dtos;
using FurnitureShop.Files.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Files.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
    private IFileService _fileService;
    private readonly DbSet<AppUser> _dbSet;

    public FilesController(IFileService fileService, AppDbContext context)
    {
        _fileService = fileService;
        _dbSet = context.Users;
    }

    [HttpPost]
    [ProducesResponseType(typeof(FilesView), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddFiles([FromBody] AddFiles dto)
    {
        var saveFile = await _fileService.AddFilesAsync(dto);
        if (saveFile is null)
            return BadRequest();

        return Ok(saveFile);
    }

    [HttpGet("users/avatar")]
    public async Task<ActionResult> GetUserAvatarAsync([FromQuery]string? userId)
    {
        if(string.IsNullOrWhiteSpace(userId))
            return BadRequest();

        var user = await _dbSet.FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));
        if(user is null)
            return await _fileService.GetUserAvatarAsync("avatar.png");

        var fileName = user.AvatarUrl;

        var avatar = await _fileService.GetUserAvatarAsync(fileName);

        return avatar;
    }

}
