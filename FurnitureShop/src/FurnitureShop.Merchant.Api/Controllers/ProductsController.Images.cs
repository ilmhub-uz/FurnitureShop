using FurnitureShop.Data.Entities;
using FurnitureShop.Merchant.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Merchant.Api.Controllers;

public partial class ProductsController
{
    [HttpPost("{productId}/images")]
    public async Task<IActionResult> AddProductImage(Guid productId, [FromForm] CreateProductImageDto imageDto)
    {
        /*var product = new Product()
        {
            Id = Guid.NewGuid(),
            Name = "dafaada",
            Description = "dadas",
            Price = 1000,
            Properties = new Dictionary<string, string>()
            {
                { "rangi", "qora" },
                { "xotira", "12 gb" }
            }
        };

        _context.Products!.Add(product);

        await _context.SaveChangesAsync();*/

        if (imageDto.ImageFile is null)
            return BadRequest();

        productId = Guid.Parse("faf186a9-45ac-4391-b93d-e584aeba9971");

        var productImage = new ProductImage()
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            Order = 0
        };

        productImage.ImagePath = $"{productImage.Id:n}.png";

        var ms = new MemoryStream();
        await imageDto.ImageFile.CopyToAsync(ms);

        await System.IO.File.WriteAllBytesAsync(Path.Combine("wwwroot", "Products", productImage.ImagePath), ms.ToArray());

        _context.ProductImages!.Add(productImage);
        await _context.SaveChangesAsync();

        return Ok($"/products/{productImage.ImagePath}");
    }


    [HttpGet("{productId}/images")]
    public async Task<IActionResult> GetProductImages()
    {
        return Ok();
    }
}