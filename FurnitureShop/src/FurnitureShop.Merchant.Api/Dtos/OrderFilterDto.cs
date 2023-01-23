using FurnitureShop.Common.Models;

namespace FurnitureShop.Merchant.Api.Dtos;

public class OrderFilterDto : PaginationParams
{
    public Guid? ProductId { get; set; }
    public DateTime? CreatedAt { get; set; }
}
