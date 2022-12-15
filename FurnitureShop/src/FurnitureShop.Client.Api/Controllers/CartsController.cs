using System.Security.Claims;
using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.Services;
using FurnitureShop.Client.Api.Services.Interfaces;
using FurnitureShop.Client.Api.ViewModel;
using FurnitureShop.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Client.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartsController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartsController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [ProducesResponseType(typeof(List<CartView>), StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<CartView>> GetCarts() =>
        await _cartService.GetUserCart();

    [ProducesResponseType(typeof(List<CartView>), StatusCodes.Status200OK)]
    [HttpPost]
    public async Task<ActionResult<CartView>> AddToCart(ClaimsPrincipal claimsPrincipal, Guid productId, CreateCartDto createCartDto)
    {
        await _cartService.AddToCart(claimsPrincipal, productId, createCartDto);
        return Ok();
    }

    [ProducesResponseType(typeof(CartView), StatusCodes.Status200OK)]
    [HttpGet("{cartProductId}")]
    public async Task<ActionResult<CartView>> DeleteCartProductById(Guid cartProductId, Guid productId)
    {
        await _cartService.DeleteCartProductById(cartProductId, productId);
        return Ok();
    }

    [ProducesResponseType(typeof(List<CartView>), StatusCodes.Status200OK)]
    [HttpPut("{cartProductId}")]
    public async Task<ActionResult<List<CartView>>> DeleteCart(Guid cartProductId)
    {
        await _cartService.DeletCartAllProducts(cartProductId);
        return Ok();
    }
}
