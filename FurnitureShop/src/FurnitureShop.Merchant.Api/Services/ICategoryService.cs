using FurnitureShop.Merchant.Api.ViewModel;

namespace FurnitureShop.Merchant.Api.Services
{
    public interface ICategoryService
    {
        public List<CategoryView> GetAllCategories();
    }
}
