using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.Services.Interfaces;
using FurnitureShop.Client.Api.ViewModel;
using FurnitureShop.Common.Models;
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
        private readonly IFavouriteService _favouriteService;


        public FavouriteController(IFavouriteService favouriteService)
        {
            _favouriteService = favouriteService;
        }



        [ProducesResponseType(typeof(FavouriteView), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<FavouriteView>> GetUserFavourite([FromQuery] PaginationParams paginationParams)
        {
            return Ok(await _favouriteService.GetUserFavourite(paginationParams, User));
        }

        [ProducesResponseType(typeof(FavouriteView), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<FavouriteView>> AddToFavourite(CreateFavouriteDto dtoModel)
        {
            await _favouriteService.AddToFavourite(User, dtoModel);
            return Ok();
        }


        [ProducesResponseType(typeof(FavouriteView), StatusCodes.Status200OK)]
        [HttpDelete]
        public async Task<ActionResult<FavouriteView>> DeleteFromFavouriteAllProducts()
        {
            await _favouriteService.DeleteFromFavouriteAllProducts(User);
            return Ok();
        }

        [ProducesResponseType(typeof(FavouriteView), StatusCodes.Status200OK)]
        [HttpDelete("{productId}")]
        public async Task<ActionResult<FavouriteView>> DeleteFromCartProductById(Guid productId)
        {
            await _favouriteService.DeleteFromFavouriteProductById(User, productId);
            return Ok();
        }

    }
}
