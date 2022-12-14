using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.Filters;
using FurnitureShop.Client.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Client.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartsController : ControllerBase
{
    [ProducesResponseType(typeof(List<CartView>), StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<IActionResult> GetCarts()
    {
        return Ok();
    }

    [ProducesResponseType(typeof(List<CartView>), StatusCodes.Status200OK)]
    [HttpPost]
    public async Task<IActionResult> AddToCart([FromBody] CreateCartDto createCartDto)
    {
        return Ok();
    }

    [ProducesResponseType(typeof(List<CartView>), StatusCodes.Status200OK)]
    [HttpPut("{cartProductId}")]
    [TypeFilter(typeof(DeleteCartFilterAttribute))]
    public async Task<IActionResult> DeleteCart(Guid cartProductId)
    {
        return Ok();
    }
}