﻿using System.Security.Claims;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Common.Filters;
using FurnitureShop.Common.Helpers;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.Dtos.Enums;
using FurnitureShop.Merchant.Api.ViewModel;
using JFA.DependencyInjection;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Merchant.Api.Services;

[Scoped]
public class OrganizationService : IOrganizationService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrganizationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<OrganizationView>> GetOrganizationsAsync(OrganizationSortingFilter filter)
    {
        var existingOrganizations = _unitOfWork.Organizations.GetAll();

        if(filter.OrganizationId is not null)
            existingOrganizations = existingOrganizations.Where(organization => organization.Id == filter.OrganizationId);
        
        if(filter.Status != null)
        {
            existingOrganizations = filter.Status switch
            {
                EOrganizationStatus.Created => existingOrganizations = 
                    existingOrganizations.Where(o => o.Status == EOrganizationStatus.Created),
                
                EOrganizationStatus.Active => existingOrganizations = 
                    existingOrganizations.Where(o => o.Status == EOrganizationStatus.Active),

                EOrganizationStatus.InActive => existingOrganizations = 
                    existingOrganizations.Where((o) => o.Status == EOrganizationStatus.InActive),

                EOrganizationStatus.Deleted => existingOrganizations = 
                    existingOrganizations.Where(o => o.Status == EOrganizationStatus.Deleted),

                _ => existingOrganizations,
            };
        }

        if(filter.SortingParams != null)
        {
            existingOrganizations = filter.SortingParams switch
            {
                ESortingParameters.Name => existingOrganizations.OrderByDescending(o => o.Name),
                _ => existingOrganizations,
            };
        }

        var organizations = await existingOrganizations.ToPagedListAsync(filter);

        return organizations.Adapt<List<OrganizationView>>();
    }

    [IdValidation]
    public async Task<OrganizationView> GetOrganizationByIdAsync(Guid organizationId)
    {
        var organization = await _unitOfWork.Organizations.GetAll().FirstOrDefaultAsync(org => org.Id == organizationId);

        return organization!.Adapt<OrganizationView>();
    }

    [Authorize]
    public async Task AddOrganization(ClaimsPrincipal claims, CreateOrganizationDto createOrganizationDto)
    {
        var organization = createOrganizationDto.Adapt<Organization>();
        var userId = Guid.Parse(claims.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        organization.Users = new List<OrganizationUser>()
        {
            new OrganizationUser()
            {
                Role = ERole.Owner,
                UserId = userId,
            }
        };

        await _unitOfWork.Organizations.AddAsync(organization);
    }

    public async Task UpdateOrganization(Guid organizationId, UpdateOrganizationDto updateOrganizationDto)
    {
        var organization = await _unitOfWork.Organizations.GetAll().FirstOrDefaultAsync(org => org.Id == organizationId);

        organization!.Name = updateOrganizationDto.Name;

        await _unitOfWork.Organizations.Update(organization);
    }

    public async Task DeleteOrganization(Guid organizationId)
    {
        var organization = await _unitOfWork.Organizations.GetAll().FirstOrDefaultAsync(org => org.Id == organizationId);

        await _unitOfWork.Organizations.Remove(organization!);
    }

    public async Task<OrganizationView> GetOrganizationByNameAsync(string organizationName)
    {
        var organization = await _unitOfWork.Organizations.GetAll()
            .FirstOrDefaultAsync(org => org.Name == organizationName);
        if(organization is null)
            throw new NotFoundException<Organization>();

        return organization.Adapt<OrganizationView>();
    }
}