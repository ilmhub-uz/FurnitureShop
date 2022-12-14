using System.Diagnostics.CodeAnalysis;
using FurnitureShop.Client.Api.Services.Interfaces;
using FurnitureShop.Data.Entities;
using JFA.DependencyInjection;

namespace FurnitureShop.Client.Api.Services;

[Scoped]
public class FileHelperService : IFileHelperService
{
    public async Task<string?> SaveFileAsync([NotNull] IFormFile file, EFileType fileTypeEnum, EFileFolder fileFolderEnum)
    {
        var fileFolder = fileFolderEnum.ToString();
        var fileType = fileTypeEnum.ToString();

        var path = Path.Combine("wwwroot", fileType, fileFolder);
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);

        var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        System.IO.File.WriteAllBytes(Path.Combine(path, fileName), ms.ToArray());

        return fileName;
    }
}
