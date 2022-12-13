using FurnitureShop.Data.Entities;

namespace FurnitureShop.Api.Services;

public interface IFileHelperService
{
    Task<string?> SaveFileAsync(IFormFile file, EFileType fileTypeEnum, EFileFolder fileFolderEnum);
}
