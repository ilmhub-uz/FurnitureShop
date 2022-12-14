using FurnitureShop.Client.Api.Services.Interfaces;
using FurnitureShop.Client.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Client.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesService _categoriesService;

    public CategoriesController(ICategoriesService categoriesService) => 
        _categoriesService = categoriesService;

    [HttpGet]
    [ProducesResponseType(typeof(List<CategoryView>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrganizations() => 
        Ok(await _categoriesService.GetCategoriesAsync());

    [HttpGet("{categoryId:int}")]
    [ProducesResponseType(typeof(CategoryView), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategoryById(int categoryId) => 
        Ok(await _categoriesService.GetCategoryByIdAsync(categoryId));
}