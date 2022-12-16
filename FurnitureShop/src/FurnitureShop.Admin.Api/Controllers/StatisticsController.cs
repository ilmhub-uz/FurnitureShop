using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.Services;
using FurnitureShop.Common.Exceptions;
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
        public async Task<IActionResult> GetProducts(PaginationParams pagination)
        {
            List<MostSoldProducts> mostSoldProducts = new();
            var contracts = _unitOfWork.Contracts.GetAll().OrderByDescending(c=>c.ProductCount);
            foreach (var contract in await contracts.ToPagedListAsync(pagination))
            {
                mostSoldProducts.Add(new MostSoldProducts()
                {
                   Name = contract.Product!.Name,
                   SoldCount = contract.ProductCount,
                   TotalSales = contract.TotalPrice
                });
            }
            return Ok(mostSoldProducts);
        }

        [HttpGet]
        public async Task<IActionResult> GetTotalSales([FromQuery] OrganizationFilterDto filter)
        {
            var contracts =  _unitOfWork.Contracts.GetAll();
            var totalSales = new TotalSales();

            if(filter.OrganizationId is not null)
            {
                var organization = await _unitOfWork.Organizations.GetAll().FirstOrDefaultAsync(o => o.Id == filter.OrganizationId);
                if (organization is null)
                    throw new NotFoundException<Organization>();
                contracts = contracts.Where(c => c.Product!.OrganizationId == filter.OrganizationId);

                totalSales.Name = organization.Name!;
            }
            
            totalSales.TotalSale =  await contracts.Select(c => c.TotalPrice).SumAsync();
            return Ok(totalSales);
        }

        [HttpGet]
        public async Task<IActionResult> GetTotalOrders([FromQuery]OrderFilterDto filter)
        {
            var orders = _unitOfWork.Orders.GetAll();
            var ordersCount = new OrdersCount();

            if(filter.OrganizationId!=null && filter.Status != null)
            {
                orders = orders.Where(o => o.Status == filter.Status && o.OrganizationId==filter.OrganizationId);
                ordersCount.Status = filter.Status.ToString()!;
                ordersCount.OrganizationId = filter.OrganizationId;
            }
            if(filter.Status is not null)
            {
                orders = orders.Where(o => o.Status == filter.Status);
                ordersCount.Status = filter.Status.ToString()!;
            }

            ordersCount.TotalCount = await orders.CountAsync();
            return Ok(ordersCount);
        }
    }   
}
