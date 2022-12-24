using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.ViewModel;
using FurnitureShop.Data.Entities;

namespace FurnitureShop.Admin.Api.Services;

public interface IUserService
{
    Task<List<UserView>> GetUsers(UserFilterDto userFilterDto);
    Task<UserView> GetUserById(Guid userId);
    Task UpdateUser(Guid userId, UpdateUserDto updateUserDto);
    Task DeleteUser(Guid userId);
}
