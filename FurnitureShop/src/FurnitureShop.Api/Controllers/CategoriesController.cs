using FurnitureShop.Api.Services;
using FurnitureShop.Api.ViewModel;
using FurnitureShop.Common.Filters;
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
    public async Task<IActionResult> GetOrganizations([FromQuery] PaginationParams paginationParams) => 
        Ok(await _categoriesService.GetCategoriesAsync(paginationParams));

    [HttpGet("{categoryId:int}/children")]
    [IdValidation]
    [ProducesResponseType(typeof(List<CategoryView>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategoryChildren(int categoryId) => 
        Ok(await _categoriesService.GetCategoryChildrenAsync(categoryId));
}