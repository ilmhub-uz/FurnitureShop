using FurnitureShop.Common.Models;
using FurnitureShop.Data.Entities;

namespace FurnitureShop.Admin.Api.Dtos;

public class UserFilterDto : PaginationParams
{
    public Guid? OrganizationId { get; set; }
    public DateTime? CreatedDate { get; set; }
    public EUserStatus? UserStatus { get; set; }
}
