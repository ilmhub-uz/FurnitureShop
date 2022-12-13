using FurnitureShop.Files.Api.Dtos;

namespace FurnitureShop.Files.Api.Services;

public interface IFileHelper
{
    Task<List<string>> SaveFileAsync(List<IFormFile> files, string filesType, string filesFolder);
}
