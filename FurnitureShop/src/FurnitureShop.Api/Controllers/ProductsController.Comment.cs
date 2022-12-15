using FurnitureShop.Api.Dtos;
using FurnitureShop.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace FurnitureShop.Api.Controllers;
[Route("api/[controller]")]
public partial class ProductsController
{
    [HttpGet("{productId}")]
    [ProducesResponseType(typeof(List<ProductComment>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductComments(Guid productId)
    {
        return Ok(await _productCommentService.GetProductComments(productId));
    }

    [HttpPost("{productId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddProductComment(ClaimsPrincipal? claims, Guid productId,[FromBody] CreateProductComment commentDto)
    { 
        var result = _validator.Validate(commentDto);

        if (!result.IsValid)
            return BadRequest();

        var user = _userManager.GetUserAsync(User);
        var claim = User;
        await _productCommentService.AddProductComments(claim, productId, commentDto);

        return Ok();
    }
}
