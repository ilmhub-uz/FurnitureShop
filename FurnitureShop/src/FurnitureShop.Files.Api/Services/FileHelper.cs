using FurnitureShop.Files.Api.Dtos;
using System.Diagnostics.CodeAnalysis;

namespace FurnitureShop.Files.Api.Services;

public class FileHelper : IFileHelper
{
    public async Task<List<string>> SaveFileAsync([NotNull] List<IFormFile> files, string filesType, string filesFolder)
    {
        var path = Path.Combine("wwwroot", filesType, filesFolder);
        if(!Directory.Exists(path))
            Directory.CreateDirectory(path);

        var filePath = new List<string>();

        foreach(var file in files)
        {
            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);

            var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            await System.IO.File.WriteAllBytesAsync(Path.Combine(path, fileName), ms.ToArray());

            filePath.Add(fileName);
        }

        return filePath;
    }
}
