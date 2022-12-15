using FurnitureShop.Admin.Api.Services;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Admin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatisticsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetPopularCategories()
        {
            List<Tuple<string, int>> categoryNameWithViews = new();
            var categories =  _unitOfWork.Categories.GetAll();

            if (categories is not null)
            {
                var popularCategories = await categories.OrderByDescending(c => c.Views).Take(5).ToListAsync();
                foreach (var category in popularCategories)
                {
                    Tuple<string, int> categoryNameWithView = new Tuple<string, int>(category.Name, category.Views);
                    categoryNameWithViews.Add(categoryNameWithView);
                }
            }
            return Ok(categoryNameWithViews);
            
        }
    }
}
