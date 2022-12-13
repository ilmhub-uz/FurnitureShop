using FurnitureShop.Api.ViewModel;

namespace FurnitureShop.Api.Services;

public interface ICategoriesService
{
    Task<List<CategoryView>> GetCategoriesAsync();
    Task<CategoryView> GetCategoryByIdAsync(int categoryId);
}