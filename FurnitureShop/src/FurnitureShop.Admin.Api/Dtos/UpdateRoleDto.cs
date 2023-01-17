using FurnitureShop.Data.Entities;

namespace FurnitureShop.Admin.Api.Dtos;

public class UpdateRoleDto
{
    public string? Name { get; set; }
    public List<EPermission>? Permissions { get; set; }
}