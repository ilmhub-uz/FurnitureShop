using FurnitureShop.Api.Services;
using FurnitureShop.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class SearchController : Controller
{
    private readonly SearchService searchService;
    public SearchController(SearchService _searchService) => searchService = _searchService;

    [HttpGet]
    [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult SearchByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return BadRequest();

        var products = searchService.SearchbyName(name);
        if (products.Count is 0) return NotFound();
        else return Ok(products);
    }
}