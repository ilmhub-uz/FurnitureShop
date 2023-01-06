using FurnitureShop.Data.Entities;

namespace FurnitureShop.Merchant.Api.Services
{
    public interface ICategoryService
    {
        public List<Category> GetAllCategories();
    }
}
