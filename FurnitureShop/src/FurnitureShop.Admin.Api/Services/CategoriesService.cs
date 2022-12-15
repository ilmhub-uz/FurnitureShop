using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.ViewModel;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Common.Helpers;
using FurnitureShop.Common.Models;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using JFA.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Admin.Api.Services;

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
        var categories = await _unitOfWork.Categories.GetAll().Where(c => c.ParentId == null).ToPagedListAsync(paginationParams);

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

    public async Task AddCategory(CreateCategoryDto categoryDto)
    {
        var category = new Category()
        {
            Name = categoryDto.Name,
            ParentId = categoryDto.ParentId,
        };

        await _unitOfWork.Categories.AddAsync(category);
    }

    public async Task UpdateCategory(int categoryId, UpdateCategoryDto updateCategoryDto)
    {
        var category = await _unitOfWork.Categories.GetAll().FirstOrDefaultAsync(c => c.Id == categoryId);

        if (category is null)
            throw new NotFoundException<Category>();

        category.Name = updateCategoryDto.Name;
        category.ParentId = updateCategoryDto.ParentId;

        await _unitOfWork.Categories.Update(category);
    }

    public async Task DeleteCategory(int categoryId)
    {
        var category = await _unitOfWork.Categories.GetAll().FirstOrDefaultAsync(c => c.Id == categoryId);

        if (category is null)
            throw new NotFoundException<Category>();

        await _unitOfWork.Categories.Remove(category);
    }
}
