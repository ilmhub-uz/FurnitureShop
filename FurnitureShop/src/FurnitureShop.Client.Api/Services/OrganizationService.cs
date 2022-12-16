using FurnitureShop.Client.Api.Services.Interfaces;
using FurnitureShop.Client.Api.ViewModel;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Common.Helpers;
using FurnitureShop.Common.Models;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using JFA.DependencyInjection;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Client.Api.Services;

[Scoped]
public class OrganizationService : IOrganizationService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrganizationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<OrganizationView>> GetOrganizationsAsync()
    {
        return (await _unitOfWork.Organizations.GetAll().ToListAsync()).Adapt<List<OrganizationView>>();
    }

    public async Task<OrganizationView> GetOrganizationByIdAsync(Guid organizationId)
    {
        var organization = await _unitOfWork.Organizations.GetAll().FirstOrDefaultAsync(org => org.Id == organizationId);
        if (organization is null)
            throw new NotFoundException<Organization>();

        return organization.Adapt<OrganizationView>();
    }

    public async Task<List<OrganizationProductsView>> GetOrganizationProducts(Guid organizationId, PaginationParams paginationParams)
    {
        var organization = await _unitOfWork.Organizations.GetAll().FirstOrDefaultAsync(org => org.Id == organizationId);
        if (organization is null)
        {
            throw new NotFoundException<Organization>();
        }

        var pagedProducts = organization.Products?.AsQueryable().ToPagedList(paginationParams);
        if (pagedProducts is null)
        {
            throw new NotFoundException<Product>();
        }

        var result = pagedProducts.Adapt<List<OrganizationProductsView>>();
        return result;
    }
}