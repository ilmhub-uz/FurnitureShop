using FurnitureShop.Client.Api.Services;
using FurnitureShop.Client.Api.ViewModel;
using FurnitureShop.Common.Filters;
using FurnitureShop.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Client.Api.Controllers;

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

        if (product.Rates is { Count: > 0 })
        {
            productView.Rate = product.Rates.Select(t => (int)t).ToList().Sum() / product.Rates.Count();
        }

        await _context.SaveChangesAsync();
        return productView;
    }
}