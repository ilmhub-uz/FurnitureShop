using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.ViewModel;
using FurnitureShop.Common.Models;
using FurnitureShop.Data.Entities;

namespace FurnitureShop.Admin.Api.Services;

public interface IRolesService
{
    Task<List<RoleView>> GetRolesAsync(PaginationParams paginationParams);
    Task<AppUserRole> GetRoleByIdAsync(Guid aspUserRoleId);
}