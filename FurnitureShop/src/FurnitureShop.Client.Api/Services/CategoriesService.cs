using FurnitureShop.Client.Api.Services.Interfaces;
using FurnitureShop.Client.Api.ViewModel;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using JFA.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Client.Api.Services;

[Scoped]
public class CategoriesService : ICategoriesService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoriesService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<CategoryView>> GetCategoriesAsync()
    {
        var categories = await _unitOfWork.Categories.GetAll().Where(c => c.ParentId == null).ToListAsync();

        var categoriesViewList = new List<CategoryView>();

        foreach (var category in categories)
        {
            var categoryView = ConvertToCategoryView(category);

            categoriesViewList.Add(categoryView);
        }

        return categoriesViewList;
    }

    private CategoryView ConvertToCategoryView(Category category)
    {
        var categoryView = new CategoryView()
        {
            Id = category.Id,
            Name = category.Name,
        };

        if (category.Children is null)
            return categoryView;

        foreach (var child in category.Children)
        {
            categoryView.Children ??= new List<CategoryView>();
            categoryView.Children.Add(ConvertToCategoryView(child));
        }

        return categoryView;
    }

    public async Task<CategoryView> GetCategoryByIdAsync(int categoryId)
    {
        var category = await _unitOfWork.Categories.GetAll().FirstOrDefaultAsync(c => c.Id == categoryId);

        if (category is null)
            throw new NotFoundException<Category>();

        return ConvertToCategoryView(category);
    }
}