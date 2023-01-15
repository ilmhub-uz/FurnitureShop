using FluentValidation;
using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.Services.Interfaces;
using FurnitureShop.Client.Api.ViewModel;
using FurnitureShop.Common.Models;
using FurnitureShop.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FurnitureShop.Client.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FavouriteController : ControllerBase
{
    private readonly IFavouriteService _favouriteService;
    private readonly IValidator<CreateFavouriteDto> createfavouritedtovalidator;

    public FavouriteController(IFavouriteService favouriteService, IValidator<CreateFavouriteDto> createfavouritedtovalidator)
    {
        _favouriteService = favouriteService;
        this.createfavouritedtovalidator = createfavouritedtovalidator;
    }

    [ProducesResponseType(typeof(List<FavouriteProductView>), StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<IActionResult> GetUserFavourite([FromQuery] PaginationParams paginationParams)
    {
        return Ok(await _favouriteService.GetUserFavourite(paginationParams, User));
    }

    [HttpPost]
    public async Task<IActionResult> AddToFavourite(CreateFavouriteDto dtoModel)
    {
        var result = createfavouritedtovalidator.Validate(dtoModel);
        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        await _favouriteService.AddToFavourite(User, dtoModel);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteFromFavouriteAllProducts()
    {
        await _favouriteService.DeleteFromFavouriteAllProducts(User);
        return Ok();
    }

    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteFromCartProductById(Guid productId)
    {
        await _favouriteService.DeleteFromFavouriteProductById(User, productId);
        return Ok();
    }

}
