using FurnitureShop.Data.Entities;

namespace FurnitureShop.Admin.Api.Services
{
    public interface IFileHelperService
    {
        Task<string?> Saved(IFormFile formFile, EFileType FileType, EFileFolder fileFolder);
    }
        
   
    
}
