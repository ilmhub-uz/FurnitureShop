using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using JFA.DependencyInjection;

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

        public List<Category> GetAllCategories() 
            => unitOfWork.Categories.GetAll().ToList();
    }
}
