using FurnitureShop.Data.Entities;

namespace FurnitureShop.Client.Api.Services.Interfaces;

public interface IFileHelperService
{
    Task<string?> SaveFileAsync(IFormFile file, EFileType fileTypeEnum, EFileFolder fileFolderEnum);
}
