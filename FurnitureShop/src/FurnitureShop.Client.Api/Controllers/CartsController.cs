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
    [HttpGet]
    public async Task<ActionResult<List<CartProductView>>> GetCartsProduct([FromQuery] PaginationParams paginationParams, Guid cartId)
    {
        return Ok(await _cartService.GetUserCart(paginationParams, cartId));
    }

    [ProducesResponseType(typeof(List<CartView>), StatusCodes.Status200OK)]
    [HttpPost]
    public async Task<ActionResult<CartView>> AddToCart(Guid cartId, CreateCartProductDto createCartDto)
    {
        var result = _createUserValidator.Validate(createCartDto);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        await _cartService.AddToCart(User, cartId, createCartDto);
        return Ok();
    }

    [ProducesResponseType(typeof(CartView), StatusCodes.Status200OK)]
    [HttpDelete("{cartId}/products/{productId}")]
    public async Task<ActionResult<CartView>> DeleteCartProductById(Guid cartId, Guid productId)
    {
        await _cartService.DeleteCartProductById(cartId, productId);
        return Ok();
    }

    [ProducesResponseType(typeof(List<CartView>), StatusCodes.Status200OK)]
    [HttpDelete("{cartId}")]
    public async Task<ActionResult<List<CartView>>> DeleteCart(Guid cartId)
    {
        await _cartService.DeletCartAllProducts(cartId);
        return Ok();
    }
}
