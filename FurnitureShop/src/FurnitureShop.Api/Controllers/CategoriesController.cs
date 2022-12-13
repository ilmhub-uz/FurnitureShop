using FurnitureShop.Api.Services;
using FurnitureShop.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Api.Controllers;

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