using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using FurnitureShop.Data.Context;
using FurnitureShop.Data.Repositories.ConcreteTypeRepositories;
using JFA.DependencyInjection;

namespace FurnitureShop.Data.Repositories;

[Scoped]
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    private ICategoryRepository _categoryRepository;
    public ICategoryRepository Categories
    {
        get
        {
            if (_categoryRepository is null) _categoryRepository = new CategoryRepository(_context);
            return _categoryRepository;
        }
    }

    private IProductRepository _productRepository;
    public IProductRepository Products
    {
        get
        {
            if (_productRepository is null) _productRepository = new ProductRepository(_context);
            return _productRepository;
        }
    }

    private IOrganizationRepository _organizationRepository;
    public IOrganizationRepository Organizations
    {
        get
        {
            if (_organizationRepository is null) _organizationRepository = new OrganizationRepository(_context);
            return _organizationRepository;
        }
    }

    private IOrderRepository _orderRepository;
    public IOrderRepository Orders
    {
        get
        {
            if (_orderRepository is null) _orderRepository = new OrderRepository(_context);
            return _orderRepository;
        }
    }

    private ICartRepository _cartRepository;
    public ICartRepository Carts
    {
        get
        {
            if (_cartRepository is null) _cartRepository = new CartRepository(_context);
            return _cartRepository;
        }
    }

    private IFavoriteRepository _favoriteRepository;
    public IFavoriteRepository Favorites
    {
        get
        {
            if(_favoriteRepository is null ) _favoriteRepository = new FavoriteRepository(_context);
            return _favoriteRepository;    
        }
    }

    public IAppUserRepository _appUserRepository;
    public IAppUserRepository AppUsers
    {
        get
        {
            if(_appUserRepository is null ) _appUserRepository = new AppUserRepository(_context);
            return _appUserRepository ;
        }
    }

    private IContractRepository _contractRepository;
    public IContractRepository Contracts 
    {
        get
        {
            if( _contractRepository is null ) _contractRepository = new ContractRepository(_context);
            return _contractRepository;
        }
    }

    private ICartProductRepository _cartProductRepository;
    public ICartProductRepository CartProduct
    {
        get
        {
            if(_cartProductRepository is null) _cartProductRepository = new CartProductRepository(_context);
            return _cartProductRepository;
        }
    }


    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public int Save() => _context.SaveChanges();

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
