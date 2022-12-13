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

    public ProductsController(
        IProductService productService,
        AppDbContext appDbContext)
    {
        _productService = productService;
        _context = appDbContext;
    }

    [HttpPost]
    public async Task<IActionResult> PostProduct(CreateProductDto dtoModel)
    {  
        await _productService.AddProduct(dtoModel);

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductView>>> GetAllProducts()
    => await _productService.GetProducts();

    [HttpGet("{productId:guid}")]
    public async Task<ActionResult<ProductView>> GetProductById(Guid productId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        if (product is null)
            return NotFound();

        product.Views += 1;
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
    public IActionResult RateProduct(Guid productId, uint rate)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == productId);
        if (product is null)
            return NotFound();
        
        if (product.Rates is null) 
            product.Rates = new List<uint>();
        
        product.Rates.Add(rate);
        return Ok();
    }

    [HttpPut("{productId:guid}")]
    public async Task<IActionResult> UpdateProduct(Guid productId, UpdateProductDto dtoModel)
    {
        await _productService.UpdateProduct(productId, dtoModel);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(Guid productId)
    {
        await _productService.DeleteProductById(productId);

        return Ok();
    }
}