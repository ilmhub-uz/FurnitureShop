﻿using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.Services.Interfaces;
using FurnitureShop.Client.Api.ViewModel;
using FurnitureShop.Common.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Client.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[ValidateModel]
public partial class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IProductCommentService _productCommentService;
    public ProductsController(IProductService productService, IProductCommentService productCommentService)
    {
        _productService = productService;
        _productCommentService = productCommentService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ProductView>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ProductView>>> GetAllProducts([FromQuery] ProductFilterDto filter)
    => await _productService.GetProducts(filter);

    [HttpGet("{productId:guid}")]
    [ProducesResponseType(typeof(ProductView), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductView>> GetProductById(Guid productId)
    => await _productService.GetProductByIdAsync(productId);
}
