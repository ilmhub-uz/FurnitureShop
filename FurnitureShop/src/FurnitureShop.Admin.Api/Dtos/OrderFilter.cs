using FurnitureShop.Common.Models;

namespace FurnitureShop.Admin.Api.Dtos;

public class OrderFilter : PaginationParams
{
    public Guid? OrganizationId { get; set; }
}