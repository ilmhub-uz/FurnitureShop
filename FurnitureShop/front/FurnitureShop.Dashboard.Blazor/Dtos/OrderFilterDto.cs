using FurnitureShop.Dashboard.Blazor.Dtos.Enums;
using FurnitureShop.Dashboard.Blazor.Models;

namespace FurnitureShop.Dashboard.Blazor.Dtos;

public class OrderFilterDto : PaginationParams
{
    public Guid? OrganizationId { get; set; }
    public Guid? UserId { get; set; }
    public Guid? ProductId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public Guid? ContractId { get; set; }

    public EOrderStatus? OrderStatus { get; set; }
}