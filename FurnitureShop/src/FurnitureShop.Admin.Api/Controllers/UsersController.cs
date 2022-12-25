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
    [ValidateModel]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork? unitOfWork;
        private readonly UserManager<AppUser> userManager;
        private readonly IUserService userService;
        public UsersController(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IUserService userService)
        {
            this.userService = userService;
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromQuery] RegisterUserDto addedUser)
        {
            var user = addedUser.Adapt<AppUser>();
            var result = await userManager.CreateAsync(user, addedUser.Password);
            if (!result.Succeeded) return BadRequest();
            return Ok();
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<UserView>),StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsers([FromQuery] UserFilterDto userFilterDto)
        {
            var users = await userService.GetUsers(userFilterDto);
            return Ok(users);
        }
        [HttpGet("getbyId")]
        [ProducesResponseType(typeof(UserView),StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            return Ok(await userService.GetUserById(userId));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUser(Guid userId, UpdateUserDto updateUserDto)
        {
            await userService.UpdateUser(userId, updateUserDto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            await userService.DeleteUser(userId);
            return Ok();
        }
    }
}
