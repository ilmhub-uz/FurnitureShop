using FurnitureShop.Client.Api.ViewModel;

namespace FurnitureShop.Client.Api.Services.Interfaces;

public interface ICategoriesService
{
    Task<List<CategoryView>> GetCategoriesAsync();
    Task<CategoryView> GetCategoryByIdAsync(int categoryId);
}