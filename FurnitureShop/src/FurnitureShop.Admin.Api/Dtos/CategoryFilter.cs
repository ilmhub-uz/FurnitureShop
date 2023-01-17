using FurnitureShop.Common.Models;
using FurnitureShop.Data.Entities;

namespace FurnitureShop.Admin.Api.Dtos;

public class CategoryFilter : PaginationParams
{
    public int? CategoryId { get; set; }
}