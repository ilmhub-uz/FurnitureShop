using System.Security.Claims;
using FluentValidation;
using FurnitureShop.Common.Filters;
using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.Hubs;
using FurnitureShop.Merchant.Api.Services;
using FurnitureShop.Merchant.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Merchant.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[ValidateModel]
public partial class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private IHubContext<ProductHub> _hubContext;
    private readonly AppDbContext _context;
    private readonly IValidator<UpdateProductDto> _updateProductValidator;
    private readonly IValidator<CreateProductDto> _createProductValidator;

    public ProductsController(
        IProductService productService,
        IHubContext<ProductHub> hubContext,
        AppDbContext appDbContext,
        IValidator<UpdateProductDto> updateProductValidator,
        IValidator<CreateProductDto> createProductValidator)
    {
        _productService = productService;
        _hubContext = hubContext;
        _context = appDbContext;
        _updateProductValidator = updateProductValidator;
        _createProductValidator = createProductValidator;
    }

    [Authorize(EPermission.CanCreateProduct)]
    [HttpPost]
    public async Task<IActionResult> PostProduct([FromBody]CreateProductDto dtoModel)
    {
        // var validateResult = await _createProductValidator.ValidateAsync(dtoModel);

        // if (!validateResult.IsValid)
        //     return BadRequest();

        await _productService.AddProduct(dtoModel, User);
        await _hubContext.Clients.All.SendAsync("ChangeProduct");

        return Ok();
    }

    [Authorize(EPermission.CanReadProduct)]
    [HttpGet]
    public async Task<ActionResult<List<ProductView>>> GetAllProducts([FromQuery]ProductSortingFilter sortingFilter)
    => await _productService.GetProducts(sortingFilter, User);

    [Authorize(EPermission.CanReadProduct)]
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
        await _hubContext.Clients.All.SendAsync("ChangeProduct");
        return productView;
    }

    [Authorize(EPermission.CanCreateProduct)]
    [HttpPost]
    [Route($"RateProduct")]
    [IdValidation]
    public async Task<IActionResult> RateProduct(Guid productId, uint rate)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        
        // if (product!.Rates is null) 
        //     product.Rates = new List<uint>();
        
        product.Rates.Add(rate);
        await _context.SaveChangesAsync();
        await _hubContext.Clients.All.SendAsync("ChangeProduct");
        return Ok();
    }

    [Authorize(EPermission.CanUpdateProduct)]
    [HttpPut("{productId:guid}")]
    [IdValidation]
    public async Task<IActionResult> UpdateProduct(Guid productId, [FromBody] UpdateProductDto dtoModel)
    {
        var validateResult = _updateProductValidator.Validate(dtoModel);

        // if (!validateResult.IsValid)
        //     return BadRequest();

        await _productService.UpdateProduct(productId, dtoModel, User);
        await _hubContext.Clients.All.SendAsync("ChangeProduct");

        return Ok();
    }

    [Authorize(EPermission.CanDeleteProduct)]
    [HttpDelete("{productId:guid}")]
    [IdValidation]
    public async Task<IActionResult> DeleteProduct(Guid productId)
    {
        await _productService.DeleteProductById(productId);
        await _hubContext.Clients.All.SendAsync("ChangeProduct");

        return Ok();
    }

    [Authorize(EPermission.CanUpdateProduct)]
    [HttpPut("{productId}/images")]
    public async Task<IActionResult> AddProductImage(Guid productId, [FromForm] CreateOrUpdateProductImageDto imageDto)
    {
        var result = await _productService.AddOrUpdateProductImageAsync(productId, imageDto);
        await _hubContext.Clients.All.SendAsync("ChangeProduct");

        return Ok(result);
    }
}