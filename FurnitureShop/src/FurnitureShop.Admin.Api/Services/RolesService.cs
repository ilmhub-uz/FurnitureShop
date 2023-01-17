using FurnitureShop.Admin.Api.ViewModel;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Common.Helpers;
using FurnitureShop.Common.Models;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using JFA.DependencyInjection;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Admin.Api.Services;

[Scoped]
public class RolesService : IRolesService
{
    private readonly IUnitOfWork _unitOfWork;

    public RolesService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AppUserRole> GetRoleByIdAsync(Guid aspUserRoleId)
    {
        var retrievedRole = await _unitOfWork.Roles.GetAll().FirstOrDefaultAsync(r => r.Id == aspUserRoleId);
         
        if(retrievedRole is null)
            throw new NotFoundException<AppUserRole>();

        return retrievedRole;
    }

    public async Task<List<RoleView>> GetRolesAsync(PaginationParams paginationParams)
    {
        var res = await _unitOfWork.Roles.GetAll().ToPagedListAsync(paginationParams);

        return res.Adapt<List<RoleView>>();
    }
}