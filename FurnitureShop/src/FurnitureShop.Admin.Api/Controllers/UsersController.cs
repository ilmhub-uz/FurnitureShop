using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.Services;
using FurnitureShop.Admin.Api.ViewModel;
using FurnitureShop.Common.Filters;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Admin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IUserService userService;
        public UsersController(UserManager<AppUser> userManager, IUserService userService)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize(EPermission.CanCreateUser)]
        public async Task<IActionResult> CreateUser([FromQuery] RegisterUserDto registerUser)
        {
            var user = registerUser.Adapt<AppUser>();
            var result = await userManager.CreateAsync(user, registerUser.Password);
            if (!result.Succeeded) return BadRequest();
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<UserView>), StatusCodes.Status200OK)]
        [Authorize(EPermission.CanReadUser)]
        public async Task<IActionResult> GetUsers([FromQuery] UserFilterDto userFilterDto)
        => Ok(await userService.GetUsers(userFilterDto));

        [HttpGet("{userId:guid}")]
        [ProducesResponseType(typeof(UserView), StatusCodes.Status200OK)]
        [Authorize(EPermission.CanReadUser)]
        public async Task<IActionResult> GetUserById([FromRoute] Guid userId)
        => Ok(await userService.GetUserById(userId));
        
        [HttpPut("{userId:Guid}")]
        [Authorize(EPermission.CanUpdateUser)]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid userId,[FromBody] UpdateUserDto updateUserDto)
        {
            await userService.UpdateUser(userId, updateUserDto);
            return Ok();
        }

        [HttpDelete("{userId:guid}")]
        [Authorize(EPermission.CanDeleteUser)]
        public async Task<IActionResult> DeleteUser([FromRoute]Guid userId)
        {
            await userService.DeleteUser(userId);
            return Ok();
        }
    }
}
