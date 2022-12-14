using FurnitureShop.Data.Entities;

namespace FurnitureShop.Admin.Api.Services;

public interface IFileHelperService
{
    Task<string?> SaveFileAsync(IFormFile file, EFileType fileTypeEnum, EFileFolder fileFolderEnum);
}
