using FurnitureShop.Common.Models;

namespace FurnitureShop.Admin.Api.Dtos;

public class RolesFilterDto : PaginationParams
{
    public Guid? RoleId { get; set; }
}