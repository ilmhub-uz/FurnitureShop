using FurnitureShop.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.Data.Repositories.ConcreteTypeRepositories;

public class CartProductRepository : GenericRepository<CartProductRepository>, ICartProductRepository, IEntityExistsRepository
{
    public CartProductRepository(AppDbContext context) : base(context)
    {

    }
}
