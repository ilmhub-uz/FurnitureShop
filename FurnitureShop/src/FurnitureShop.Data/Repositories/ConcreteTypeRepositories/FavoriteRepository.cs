﻿using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using JFA.DependencyInjection;

namespace FurnitureShop.Data.Repositories.ConcreteTypeRepositories;

[Scoped]
public class FavoriteRepository : GenericRepository<Favourite>, IFavoriteRepository, IEntityExistsRepository
{
	public FavoriteRepository(AppDbContext context) : base(context) { }
}
