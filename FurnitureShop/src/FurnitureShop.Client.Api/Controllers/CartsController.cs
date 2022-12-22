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

    [ProducesResponseType(typeof(CartView), StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<CartView>> GetCartsProduct([FromQuery] PaginationParams paginationParams)
    {
        return Ok(await _cartService.GetUserCart(paginationParams, User));
    }

    [ProducesResponseType(typeof(List<CartView>), StatusCodes.Status200OK)]
    [HttpPost]
    public async Task<ActionResult<CartView>> AddToCart(CreateCartProductDto createCartDto)
    {
        var result = _createUserValidator.Validate(createCartDto);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        await _cartService.AddToCart(User, createCartDto);
        return Ok();
    }

    [ProducesResponseType(typeof(CartView), StatusCodes.Status200OK)]
    [HttpDelete("{cartId}/products/{productId}")]
    public async Task<ActionResult<CartView>> DeleteCartProductById(Guid productId)
    {
        await _cartService.DeleteCartProductById(User, productId);
        return Ok();
    }

    [ProducesResponseType(typeof(List<CartView>), StatusCodes.Status200OK)]
    [HttpDelete("{cartId}")]
    public async Task<ActionResult<List<CartView>>> DeleteCart()
    {
        await _cartService.DeletCartAllProducts(User);
        return Ok();
    }
}
