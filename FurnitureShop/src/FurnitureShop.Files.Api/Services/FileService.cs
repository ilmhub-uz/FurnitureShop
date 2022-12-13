using FurnitureShop.Files.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

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
