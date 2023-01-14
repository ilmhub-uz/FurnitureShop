using FurnitureShop.Api.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Api.Controllers;

public partial class ProductsController
{
    [HttpGet("{productId:guid}/Comments")]
    [ProducesResponseType(typeof(List<ProductCommentView>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductComments(Guid productId) =>
        Ok(await _productCommentService.GetProductComments(productId));

    [Authorize]
    [HttpPost("{productId:guid}/Comment")] 
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddProductComment(Guid productId, [FromBody] CreateProductComment commentDto)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user is null)
            return BadRequest();

        await _productCommentService.AddProductComments(user, productId, commentDto);

        return Ok();
    }
}
