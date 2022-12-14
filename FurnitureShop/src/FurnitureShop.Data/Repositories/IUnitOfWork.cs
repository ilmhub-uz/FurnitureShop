﻿using FurnitureShop.Data.Repositories.ConcreteTypeRepositories;

namespace FurnitureShop.Data.Repositories;

public interface IUnitOfWork
{
    ICategoryRepository Categories { get; }
    IProductImageRepository ProductImages { get; }
    IProductRepository Products { get; }
    IOrganizationRepository Organizations { get; }
    IOrderRepository Orders { get; }
    int Save();
}