using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Client.Api.Controllers;

public partial class ProductsController
{

    [HttpGet("productComments")]
    [ProducesResponseType(typeof(List<ProductCommentView>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ProductCommentView>>> GetProductCommentsAsync()
         => await _productCommentService.GetProductCommentsAsync();

    [HttpGet("{commentId:guid}")]
    [ProducesResponseType(typeof(ProductCommentView), StatusCodes.Status200OK)]
    public async Task<ActionResult<ProductCommentView>> GetProductCommentByIdAsync(Guid commentId)
        => await _productCommentService.GetProductCommentByIdAsync(commentId);

    [HttpPost]
    public async Task<IActionResult> AddProductCommentAsync(CreateProductCommentDto commentDto)
    {
        await _productCommentService.AddProductCommentAsync(commentDto);
        return Ok();
    }

    [HttpPut("{commentId:guid}")]
    public async Task<IActionResult> UpdateProductCommentAsync(Guid commentId, UpdateProductCommentDto updateCommentDto)
    {
        var updateProductComment = await _productCommentService.UpdateProductComment(commentId, updateCommentDto);
        return Ok(updateProductComment);
    }

    [HttpDelete("{commentId:guid}")]
    public async Task<IActionResult> DeleteProductCommentAsync(Guid commentId)
    {
        await _productCommentService.DeleteProductComment(commentId);
        return Ok();
    }
}
