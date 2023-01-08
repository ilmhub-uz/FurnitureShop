using FurnitureShop.Data.Entities;
using FurnitureShop.Merchant.Api.Services;
using FurnitureShop.Merchant.Api.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Merchant.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public List<CategoryView> GetAll()
            => categoryService.GetAllCategories();
    }
}
