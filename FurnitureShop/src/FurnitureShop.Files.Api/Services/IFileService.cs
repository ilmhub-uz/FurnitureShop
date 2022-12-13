using FurnitureShop.Files.Api.Dtos;

namespace FurnitureShop.Files.Api.Services;

public interface IFileService
{
    Task<FilesView> AddFilesAsync(AddFiles files);
}
