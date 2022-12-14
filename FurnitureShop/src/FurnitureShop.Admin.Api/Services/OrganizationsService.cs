﻿using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.ViewModel;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using JFA.DependencyInjection;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Admin.Api.Services;
[Scoped]
public class OrganizationsService : IOrganizationsService
{
    private readonly UnitOfWork _unitOfWork;
    public OrganizationsService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<List<OrganizationView>> GetOrganizationsAsync()
    {
        var organization = await _unitOfWork.Organizations.GetAll().ToListAsync();
        return organization.Adapt<List<OrganizationView>>();
    }
    public async Task<OrganizationView> GetOrganizationByIdAsync(Guid organizationId)
    {
        var organization = (await _unitOfWork.Organizations.GetAll().ToListAsync())
            .FirstOrDefault(o => o.Id.Equals(organizationId));

        if (organization is null)
            throw new NotFoundException<Organization>();

        return organization.Adapt<OrganizationView>();
    }
    public async Task UpdateOrganization(Guid organizationId, UpdateOrganizationDto updateOrganizationDto)
    {
        var organization = _unitOfWork.Organizations.GetById(organizationId);
        if (organization is null)
            throw new NotFoundException<Organization>();

        organization.Name = updateOrganizationDto.Name;
        organization.Status = updateOrganizationDto.Status;

        await _unitOfWork.Organizations.Update(organization);
    }

    public async Task DeleteOrganization(Guid organizationId, DeleteOrganizationDto deleteOrganizationDto)
    {
        var organization = _unitOfWork.Organizations.GetById(organizationId);
        if (organization is null)
            throw new NotFoundException<Organization>();

        organization.Status = deleteOrganizationDto.Status;

        await _unitOfWork.Organizations.Update(organization);
    }
}
