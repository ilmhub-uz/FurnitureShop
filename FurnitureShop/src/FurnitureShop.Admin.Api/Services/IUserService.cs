using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Data.Entities;

namespace FurnitureShop.Admin.Api.Services;

public interface IUserService
{
    Task<List<AppUser>> GetUsers(UserFilterDto userFilterDto);
    Task<AppUser> GetUserById(Guid userId);
    Task UpdateUser(Guid userId, UpdateUserDto updateUserDto);
    Task DeleteUser(Guid userId);
}
