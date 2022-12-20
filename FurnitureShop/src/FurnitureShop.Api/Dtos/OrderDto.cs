using FurnitureShop.Common.Models;

namespace FurnitureShop.Api.Dtos;

public class OrderDto : PaginationParams
{
    public string? OrderBy { get; set; }
}