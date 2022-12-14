namespace FurnitureShop.Data.Repositories;
public interface IEntityExistsRepository
{
    Task<bool> IsExists(object? id);
}