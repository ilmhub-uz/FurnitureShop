using FurnitureShop.Files.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace FurnitureShop.Files.Api.Services;

public class FileService : IFileService
{
    private IFileHelper _fileHelper;

    public FileService(IFileHelper fileHelper)
    {
        _fileHelper = fileHelper;
    }

    public async Task<FilesView?> AddFilesAsync(AddFiles files)
    {
        if (files.Type is null)
            return null;

        if (files.Files is null)
            return null;

        if (files.Folder is null)
            return null;

        var filesFolder = files.Folder.ToString();
        var filesType = files.Type.ToString();

        var savedFiles = await _fileHelper.SaveFileAsync(files.Files, filesType!, filesFolder!);

        return ToDto(savedFiles, filesType!, filesFolder!);
    }

    public async Task<FileContentResult> GetUserAvatarAsync(string? fileName)
    {
        string path = Path.Combine(new string[4] { "wwwroot", EFileType.Images.ToString(), EFileFolder.User.ToString(), fileName ?? "avatar.png" });
        byte[] bytes = await System.IO.File.ReadAllBytesAsync(path);

        if (string.IsNullOrWhiteSpace(fileName))
            return new FileContentResult(bytes, "image/png");
        
        string contentType = string.Empty;
        new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType!);

        return new FileContentResult(bytes, contentType);
    }

    public FilesView ToDto(List<string> files, string filesType, string filesFolder)
    {
        return new FilesView()
        {
            Files = files,
            Type = filesType,
            Folder = filesFolder
        };
    }
}
