using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using FurnitureShop.Merchant.Api.ViewModel;
using JFA.DependencyInjection;
using Mapster;

namespace FurnitureShop.Merchant.Api.Services
{
    [Scoped]
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryService(IUnitOfWork _unitOfWork)
        {   
            unitOfWork = _unitOfWork;
        }

        public List<CategoryView> GetAllCategories()
        {
            var categories = unitOfWork.Categories.GetAll().ToList();
            return categories.Select(c => c.Adapt<CategoryView>()).ToList() ?? new List<CategoryView>();
        }
    }
}
