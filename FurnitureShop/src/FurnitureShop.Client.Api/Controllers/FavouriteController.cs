using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.Services.Interfaces;
using FurnitureShop.Client.Api.ViewModel;
using FurnitureShop.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FurnitureShop.Client.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IFavouriteService _favouriteService;


        [HttpGet]
        public async Task<ActionResult<List<FavouriteView>>> GetFavourite(Guid favouriteId)
        {
            var favourite = await _context.FavouriteProducts.FirstOrDefaultAsync(f => f.Id == favouriteId);
            if (favourite is null)
                return NotFound();

            var favouriteView = await _favouriteService.GetFavouriteByIdAsync(favouriteId);

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<List<FavouriteView>>> AddToFavourite(Guid productId, [FromBody] CreateFavouriteDto dtoModel)
        {
            await _favouriteService.AddToFavourites(User, productId, dtoModel);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult<List<FavouriteView>>> DeleteFavourite(Guid favouriteId)
        {
            await _favouriteService.RemoveFavourites(favouriteId);
            return Ok();
        }

    }
}
