using FurnitureShop.Api.ViewModel;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Common.Helpers;
using FurnitureShop.Common.Models;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using JFA.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Api.Services;

[Scoped]
public class CategoriesService : ICategoriesService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoriesService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<CategoryView>> GetCategoriesAsync(PaginationParams paginationParams)
    {
        var categories = await _unitOfWork.Categories.GetAll()
            .Where(c => c.ParentId == null)
            .ToPagedListAsync(paginationParams);

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

    public async Task<List<CategoryView>> GetCategoryChildrenAsync(int categoryId)
    {
        var categoryChildren = 
            await _unitOfWork.Categories.GetAll()
                .Where(category=>category.ParentId == categoryId)
                .ToListAsync();

        var categoryChildrenViews = 
            categoryChildren.Select(categoryChild => ConvertToCategoryView(categoryChild)).ToList();

        return categoryChildrenViews;
    }
}