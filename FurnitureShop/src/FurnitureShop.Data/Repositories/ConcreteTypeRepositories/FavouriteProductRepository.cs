﻿using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.Data.Repositories.ConcreteTypeRepositories;

public class FavouriteProductRepository : GenericRepository<FavouriteProduct>, IFavouriteProductRepository, IEntityExistsRepository
{
    public FavouriteProductRepository(AppDbContext context) : base(context)
    {

    }
}
