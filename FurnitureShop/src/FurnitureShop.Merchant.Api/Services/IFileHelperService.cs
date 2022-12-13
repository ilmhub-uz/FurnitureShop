using FurnitureShop.Data.Entities;

namespace FurnitureShop.Merchant.Api.Services;

public interface IFileHelperService
{
    Task<string?> SaveFileAsync(IFormFile file, EFileType fileTypeEnum, EFileFolder fileFolderEnum);
}
