using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Client.Api.Controllers;

public partial class ProductsController
{
    [HttpGet("products/{productId:guid}/comments")]
    [ProducesResponseType(typeof(ProductCommentView), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ProductCommentView>>> GetProductCommentsAsync(Guid productId) =>
        await _productCommentService.GetProductCommentsAsync(productId);

    [HttpPost("products/{productId:guid}/comments")]
    public async Task<IActionResult> AddProductCommentAsync(Guid productId, [FromBody] CreateProductCommentDto commentDto)
    {
        var product = await _productCommentService.AddProductCommentAsync(User, productId, commentDto);
        return Ok(product);
    }

    [HttpPut("products/{productId:guid}/comments/{commentId:guid}")]
    public async Task<IActionResult> UpdateProductCommentAsync(Guid productId, Guid commentId, [FromBody] UpdateProductCommentDto updateCommentDto)
    {
        var updateProduct = await _productCommentService.UpdateProductComment(productId, commentId, updateCommentDto);
        return Ok(updateProduct);
    }

    [HttpDelete("products/{productId:guid}/comments/{commentId:guid}")]
    public async Task<IActionResult> DeleteProductComment(Guid productId, Guid commentId)
    {
        await _productCommentService.DeleteProductComment(productId, commentId);
        return Ok();
    }
}
