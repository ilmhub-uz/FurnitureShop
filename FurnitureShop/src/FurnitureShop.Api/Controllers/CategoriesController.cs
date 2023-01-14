using FurnitureShop.Api.Services;
using FurnitureShop.Api.ViewModel;
using FurnitureShop.Common.Models;
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
    public async Task<IActionResult> GetCategories([FromQuery] PaginationParams paginationParams) =>
        Ok(await _categoriesService.GetCategoriesAsync(paginationParams));

    [HttpGet("{categoryId:int}")]
    [ProducesResponseType(typeof(CategoryView), StatusCodes.Status200OK)]
    public IActionResult GetCategoryById(int categoryId) =>
        Ok(_categoriesService.GetCategoryById(categoryId));
}