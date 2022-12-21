using FluentValidation;
using FurnitureShop.Common.Filters;
using FurnitureShop.Data.Context;
using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.Services;
using FurnitureShop.Merchant.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Merchant.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[ValidateModel]
public partial class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly AppDbContext _context;
    private readonly IValidator<UpdateProductDto> _updateProductValidator;
    private readonly IValidator<CreateProductDto> _createProductValidator;

    public ProductsController(
        IProductService productService,
        AppDbContext appDbContext,
        IValidator<UpdateProductDto> updateProductValidator,
        IValidator<CreateProductDto> createProductValidator)
    {
        _productService = productService;
        _context = appDbContext;
        _updateProductValidator = updateProductValidator;
        _createProductValidator = createProductValidator;
    }

    [HttpPost]
    public async Task<IActionResult> PostProduct([FromBody]CreateProductDto dtoModel)
    {
        var validateResult = _createProductValidator.Validate(dtoModel);

        if (!validateResult.IsValid)
            return BadRequest();

        await _productService.AddProduct(dtoModel);

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductView>>> GetAllProducts()
    => await _productService.GetProducts();

    [HttpGet("{productId:guid}")]
    [IdValidation]
    public async Task<ActionResult<ProductView>> GetProductById(Guid productId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

        product!.Views += 1;
        var productView = await _productService.GetProductByIdAsync(productId);

        if (product.Rates is {Count:>0})
        {
            productView.Rate = product.Rates.Select(t => (int)t).ToList().Sum() / product.Rates.Count();
        }

        await _context.SaveChangesAsync();
        return productView;
    }

    [HttpPost]
    [Route($"RateProduct")]
    [IdValidation]
    public async Task<IActionResult> RateProduct(Guid productId, uint rate)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        
        if (product!.Rates is null) 
            product.Rates = new List<uint>();
        
        product.Rates.Add(rate);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("{productId:guid}")]
    [IdValidation]
    public async Task<IActionResult> UpdateProduct(Guid productId, [FromBody] UpdateProductDto dtoModel)
    {
        var validateResult = _updateProductValidator.Validate(dtoModel);

        if (!validateResult.IsValid)
            return BadRequest();

        await _productService.UpdateProduct(productId, dtoModel, User);

        return Ok();
    }

    [HttpDelete]
    [IdValidation]
    public async Task<IActionResult> DeleteProduct(Guid productId)
    {
        await _productService.DeleteProductById(productId);

        return Ok();
    }

    [HttpPut("{productId}/images")]
    public async Task<IActionResult> AddProductImage(Guid productId, [FromForm] CreateOrUpdateProductImageDto imageDto)
    {
        var result = await _productService.AddOrUpdateProductImageAsync(productId, imageDto);

        return Ok(result);
    }
}