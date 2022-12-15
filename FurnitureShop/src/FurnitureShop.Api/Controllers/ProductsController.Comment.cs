using FurnitureShop.Api.Dtos;
using FurnitureShop.Api.Services;
using FurnitureShop.Common.Models;
using FurnitureShop.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public async Task<IActionResult> AddProductComment(ClaimsPrincipal claims,Guid productId, CreateProductComment commentDto)
    {
        var user = _userManager.GetUserAsync(User);

        //User - degan ozgaruvchi bor controllerni base classida unda userni claimlari saqlanadi
        //Userni type ClaimsPrinciple
        var userClaims = User;
        return Ok(await _productCommentService.AddProductComments(userClaims, productId, commentDto));
    }

}
