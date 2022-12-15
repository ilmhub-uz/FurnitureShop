using FurnitureShop.Common.Models;

namespace FurnitureShop.Admin.Api.Dtos;

public class CategoriesCountFilterDto : PaginationParams
{
    public int? CategoriesCount { get; set; }
}
