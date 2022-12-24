using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.ViewModel;
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
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<AppUser> userManager;

        public UsersController(UserManager<AppUser> userManager , IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(AddedUserDto addedUser)
        {
         
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users =  unitOfWork.AppUsers.GetAll().Adapt<List<ShowUserView>>();
            if (users is null)
                return NotFound();

            return Ok(users);
        }

    }
}
