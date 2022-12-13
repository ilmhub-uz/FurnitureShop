using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;

namespace FurnitureShop.Data.Repositories.ConcreteTypeRepositories;
public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
	public OrderRepository(AppDbContext context) :base(context) { }
}
