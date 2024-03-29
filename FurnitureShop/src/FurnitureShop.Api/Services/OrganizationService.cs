﻿using FurnitureShop.Api.ViewModel;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Common.Helpers;
using FurnitureShop.Common.Models;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using JFA.DependencyInjection;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Api.Services;

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
        var organizations = await _unitOfWork.Organizations.GetAll().ToListAsync();

        return organizations.ToList().Adapt<List<OrganizationView>>();
    }

    public async Task<OrganizationView> GetOrganizationByIdAsync(Guid organizationId)
    {
        var organization = _unitOfWork.Organizations.GetById(organizationId);

        if (organization is null)
            throw new NotFoundException<Organization>();

        return organization.Adapt<OrganizationView>();
    }
}