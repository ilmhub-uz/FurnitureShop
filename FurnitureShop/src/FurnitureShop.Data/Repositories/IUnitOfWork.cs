using FurnitureShop.Data.Repositories.ConcreteTypeRepositories;

namespace FurnitureShop.Data.Repositories;

public interface IUnitOfWork
{
    ICategoryRepository Categories { get; }
    IProductRepository Products { get; }
    IOrganizationRepository Organizations { get; }
    IOrderRepository Orders { get; }
    ICartRepository Carts { get; }
    ICartProductRepository CartProduct { get; }
    IFavoriteRepository Favorites { get; }
    IFavouriteProductRepository FavoritesProduct { get; }
    IAppUserRepository AppUsers { get ;}
    IContractRepository Contracts { get; }
    IProductCommentRepository ProductComments { get; }
    int Save();
}