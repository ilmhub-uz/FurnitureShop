using FurnitureShop.Admin.Blazor.Dtos.Enums;
using FurnitureShop.Admin.Blazor.Models;


namespace FurnitureShop.Admin.Blazor.Dtos;

public class OrderFilterDto : PaginationParams
{
    public Guid? OrganizationId { get; set; }
    public Guid? UserId { get; set; }
    public Guid? ProductId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public Guid? ContractId { get; set; }

    public EOrderStatus? OrderStatus { get; set; }
}