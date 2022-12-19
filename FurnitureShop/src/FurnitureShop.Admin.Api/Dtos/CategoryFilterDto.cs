using FurnitureShop.Common.Models;

namespace FurnitureShop.Admin.Api.Dtos
{
    public class CategoryFilterDto : PaginationParams
    {
        public int Views { get; set; }
    }
}
