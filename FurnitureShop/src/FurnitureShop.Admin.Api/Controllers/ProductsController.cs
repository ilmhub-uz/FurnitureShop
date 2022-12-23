using FluentValidation;
using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.Services;
using FurnitureShop.Admin.Api.ViewModel;
using FurnitureShop.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Admin.Api.Controllers;
[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _service;
    private readonly IValidator<UpdateProductDto> _updateproductdtovalidator;
    public ProductsController(IProductsService service, IValidator<UpdateProductDto> validator)
    {
        _updateproductdtovalidator = validator;
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ProductView>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts([FromQuery] ProductFilterDto filter)
    {
        var products = await _service.GetProducts(filter);
        return Ok(products);
    }

    [HttpGet("getbyId")]
    public async Task<IActionResult> GetProduct([FromQuery]Guid productId)
    {
        var product = await _service.GetProductByIdAsync(productId);
        return Ok(product);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromQuery]Guid productId,[FromBody] UpdateProductDto dtoModel)
    {
        var result = await _updateproductdtovalidator.ValidateAsync(dtoModel);
        if (!result.IsValid)
            return BadRequest(result.Errors);
        await _service.UpdateProduct(productId, dtoModel);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProduct([FromQuery] Guid productId)
    {
        await _service.DeleteProductById(productId);
        return NoContent();
    }
}