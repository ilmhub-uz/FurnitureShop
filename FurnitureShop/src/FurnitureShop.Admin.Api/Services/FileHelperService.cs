using FurnitureShop.Data.Entities;

namespace FurnitureShop.Admin.Api.Services
{
    public class FileHelperService : IFileHelperService
    {
        public Task<string?> Saved(IFormFile formFile, EFileType FileTypeEntity, EFileFolder fileFolderEntity)
        {
            var fileFolder = fileFolderEntity.ToString();
            var filetype = FileTypeEntity.ToString();
        }
    }
}
