using FluentValidation;
using FurnitureShop.Api.Dtos;
using FurnitureShop.Api.Services;
using FurnitureShop.Api.ViewModel;
using FurnitureShop.Common.Filters;
using FurnitureShop.Common.Models;
using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[ValidateModel]
public partial class ProductsController : ControllerBase
{

    private readonly IProductService _productService;
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly IProductCommentService _productCommentService;

    public ProductsController(
        IProductService productService,
        AppDbContext appDbContext,
        IProductCommentService productCommentService,
        UserManager<AppUser> userManager)
    {
        _productService = productService;
        _context = appDbContext;
        _productCommentService = productCommentService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductView>>> GetAllProducts([FromQuery] OrderDto orderDto)
    => await _productService.GetProductsAsync(orderDto);

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
            productView.Rate = product.Rates.Select(t => (int)t).Sum() / product.Rates.Count();
        }

        await _context.SaveChangesAsync();
        return productView;
    }
}