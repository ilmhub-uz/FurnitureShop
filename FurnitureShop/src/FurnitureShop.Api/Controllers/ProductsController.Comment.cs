using FurnitureShop.Api.Dtos;
using FurnitureShop.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Common.Filters;
using Microsoft.AspNetCore.Authorization;

namespace FurnitureShop.Api.Controllers;

public partial class ProductsController
{
    [HttpGet("{productId:guid}/Comments")]
    [ProducesResponseType(typeof(List<ProductCommentView>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductComments(Guid productId) => 
        Ok(await _productCommentService.GetProductComments(productId));

    [IdValidation]
    [Authorize]
    [HttpPost("{productId:guid}/Comment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddProductComment(Guid productId, [FromBody] CreateProductComment commentDto)
    { 
        if (!ModelState.IsValid)
            return BadRequest();

        var user = await _userManager.GetUserAsync(User);

        if (user is null)
            return BadRequest();
        
        await _productCommentService.AddProductComments(user, productId, commentDto);

        return Ok();
    }
}
