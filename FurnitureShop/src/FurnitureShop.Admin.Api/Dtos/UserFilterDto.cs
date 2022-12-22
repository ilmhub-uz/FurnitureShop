using FurnitureShop.Common.Models;

namespace FurnitureShop.Admin.Api.Dtos;

public class UserFilterDto : PaginationParams
{
    public Guid? OrganizationId { get; set; }
    public DateTime? CreatedDate { get; set; }
}
