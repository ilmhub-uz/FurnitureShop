using FurnitureShop.Api.ViewModel;
using FurnitureShop.Common.Models;

namespace FurnitureShop.Api.Services;

public interface ICategoriesService
{
    Task<List<CategoryView>> GetCategoriesAsync(PaginationParams paginationParams);
    CategoryView GetCategoryById(int categoryId);
}