using FluentValidation;
using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.Services;
using FurnitureShop.Admin.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Admin.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesService _categoriesService;
    private readonly IValidator<CreateCategoryDto> _createCategoryValidator;
    private readonly IValidator<UpdateCategoryDto> _updateCategoryValidator;

    public CategoriesController(ICategoriesService categoriesService, IValidator<UpdateCategoryDto> updateCategoryValidator, IValidator<CreateCategoryDto> createCategoryValidator)
    {
        _categoriesService = categoriesService;
        _updateCategoryValidator = updateCategoryValidator;
        _createCategoryValidator = createCategoryValidator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<CategoryView>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategories([FromQuery] CategoryFilter filter) => Ok(await _categoriesService.GetCategoriesAsync(filter));

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody]CreateCategoryDto createCategoryDto)
    {
        var result = _createCategoryValidator.Validate(createCategoryDto);
        if (!result.IsValid)
            return BadRequest(result.Errors);

        await _categoriesService.AddCategory(createCategoryDto);

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCategory([FromQuery]int categoryId, [FromBody]UpdateCategoryDto updateCategoryDto)
    {
        var result = await _updateCategoryValidator.ValidateAsync(updateCategoryDto);
        if (!result.IsValid)
            return BadRequest(result.Errors);

        await _categoriesService.UpdateCategory(categoryId, updateCategoryDto);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCategory([FromQuery] int categoryId)
    {
        await _categoriesService.DeleteCategory(categoryId);

        return Ok();
    }
}
