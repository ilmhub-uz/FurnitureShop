using FurnitureShop.Files.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Files.Api.Services;

public interface IFileService
{
    Task<FilesView> AddFilesAsync(AddFiles files);
    Task<FileContentResult?> GetUserAvatarAsync(string? fileName);
}
