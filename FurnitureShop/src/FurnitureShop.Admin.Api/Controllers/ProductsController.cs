using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.Services;
using FurnitureShop.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Admin.Api.Controllers;
[Route("api/products")]
[ApiController]

public class ProductsController : ControllerBase
{
    private readonly IProductsService _service;

    public ProductsController(IProductsService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts(ProductFilterDto product, PaginationParams pagination)
    {
        var products = await _service.GetProducts(product, pagination);
        return Ok(products);
    }

    [HttpGet("{productId:Guid}")]
    public async Task<IActionResult> GetProduct(Guid productId)
    {
        var product = await _service.GetProductByIdAsync(productId);
        return Ok(product);
    }

    [HttpPut("{productId:Guid}")]
    public async Task<IActionResult> UpdateProduct(Guid productId, UpdateProductDto dtoModel)
    {
        await _service.UpdateProduct(productId, dtoModel);
        return Ok();
    }

    [HttpDelete("{productId:Guid}")]
    public async Task<IActionResult> DeleteProduct(Guid productId)
    {
        await _service.DeleteProductById(productId);
        return NoContent();
    }
}