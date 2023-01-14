using FurnitureShop.Api.ViewModel;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Common.Helpers;
using FurnitureShop.Common.Models;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using JFA.DependencyInjection;
using Mapster;
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
            ImageUrl = category.CategoryImage?.ImagePath
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

    public CategoryView GetCategoryById(int categoryId)
    {
        var category = _unitOfWork.Categories.GetById(categoryId);

        if (category is null)
            throw new NotFoundException<Category>();

        var categoryView = ConvertToCategoryView(category);

        return categoryView;
    }
}