using FurnitureShop.Common.Models;

namespace FurnitureShop.Admin.Api.Dtos;

public class OrderFilterDto : PaginationParams
{
    public Guid? OrganizationId { get; set; }
    public Guid? UserId { get; set; }
    public Guid? ProductId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public Guid? ContractId { get; set; }

}