using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using JFA.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.Data.Repositories.ConcreteTypeRepositories;

[Scoped]
public class CartRepository : GenericRepository<Cart>, ICartRepository, IEntityExistsRepository
{
    public CartRepository(AppDbContext context) : base(context) { }
}