using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.ViewModel;

namespace FurnitureShop.Admin.Api.Services;

public interface ICategoriesService
{
    Task<List<CategoryView>> GetCategoriesAsync(CategoryFilter paginationParams);
    Task<CategoryView> GetCategoryByIdAsync(int categoryId);
    Task AddCategory(CreateCategoryDto categoryDto);
    Task UpdateCategory(int categoryId, UpdateCategoryDto updateCategoryDto);
    Task DeleteCategory(int categoryId);
}
