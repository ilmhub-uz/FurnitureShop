using System.Security.Claims;
using FluentValidation;
using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.Services;
using FurnitureShop.Client.Api.Services.Interfaces;
using FurnitureShop.Client.Api.ViewModel;
using FurnitureShop.Common.Models;
using FurnitureShop.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Client.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartsController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly IValidator<CreateCartProductDto> _createUserValidator;

    public CartsController(ICartService cartService, IValidator<CreateCartProductDto> createUserValidator)
    {
        _cartService = cartService;
        _createUserValidator = createUserValidator;
    }

    [ProducesResponseType(typeof(List<CartProductView>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
    [HttpGet]
    public async Task<IActionResult> GetUserCart([FromQuery] PaginationParams paginationParams)
    {
        return Ok(await _cartService.GetUserCart(paginationParams, User));
    }

    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
    [HttpPost]
    public async Task<IActionResult> AddToCart(CreateCartProductDto createCartDto)
    {
        var result = _createUserValidator.Validate(createCartDto);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        await _cartService.AddToCart(User, createCartDto);
        return Ok();
    }

    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteFromCartProductById(Guid productId)
    {
        await _cartService.DeleteCartProductById(User, productId);
        return Ok();
    }

    [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
    [HttpDelete]
    public async Task<IActionResult> DeleteFromCartAllProducts()
    {
        await _cartService.DeletCartAllProducts(User);
        return Ok();
    }
}
