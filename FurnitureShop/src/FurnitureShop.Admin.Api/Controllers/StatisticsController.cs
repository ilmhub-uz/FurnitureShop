using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.Services;
using FurnitureShop.Common.Helpers;
using FurnitureShop.Common.Models;
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
        public async Task<IActionResult> GetCategories(PaginationParams pagination)
        {
            List<PopularCategory> popularCategories = new();

            var categories =  _unitOfWork.Categories.GetAll().OrderByDescending(c=>c.Views);

            foreach (var category in await categories.ToPagedListAsync(pagination))
            {
                popularCategories.Add(new PopularCategory()
                {
                    CategoryName = category.Name,
                    Views = category.Views,
                });
            }
            return Ok(popularCategories);
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            _unitOfWork
        }
    }
}
