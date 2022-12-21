using FurnitureShop.Common.Models;
using FurnitureShop.Data.Entities;
using FurnitureShop.Merchant.Api.Dtos.Enums;

namespace FurnitureShop.Merchant.Api.Dtos;
public class OrganizationFilterDto : PaginationParams
{
    public Guid? OrganizationId { get; set; }
    public string? OrganizationName { get; set; }
    public DateTime? DateTime { get; set; }
    public ESortingParameters? SortingParams{ get; set; }
    public EOrganizationStatus? Status { get; set; }
}
