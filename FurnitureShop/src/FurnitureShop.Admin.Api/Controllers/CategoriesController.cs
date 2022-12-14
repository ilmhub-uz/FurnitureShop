using FluentValidation;
using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.Services;
using FurnitureShop.Admin.Api.ViewModel;
using FurnitureShop.Common.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Admin.Api.Controllers
{
    [Route("api/categories")]
    [ApiController]
    [ValidateModel]
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
        public async Task<IActionResult> GetOrganizations()
        {
            var categories = await _categoriesService.GetCategoriesAsync();

            return Ok(categories);
        }

        [HttpGet("{categoryId:int}")]
        [ProducesResponseType(typeof(CategoryView), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            var category = await _categoriesService.GetCategoryByIdAsync(categoryId);

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var result = _createCategoryValidator.Validate(createCategoryDto);
            if(!result.IsValid)
                return BadRequest(())

            await _categoriesService.AddCategory(createCategoryDto);

            return Ok();
        }

        [HttpPut("{categoryId:int}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, UpdateCategoryDto updateCategoryDto)
        {
            await _categoriesService.UpdateCategory(categoryId, updateCategoryDto);

            return Ok();
        }

        [HttpDelete("{categoryId:int}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            await _categoriesService.DeleteCategory(categoryId);

            return Ok();
        }
    }
}
