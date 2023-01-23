using FluentValidation;
using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.Services;
using FurnitureShop.Admin.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Admin.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    public async Task<IActionResult> GetProducts([FromQuery] ProductFilterDto filter) => Ok(await _service.GetProducts(filter));

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
        return Ok();
    }
}