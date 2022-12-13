namespace FurnitureShop.Files.Api.Dtos;

public class AddFiles
{
    public List<IFormFile>? Files { get; set; }
    public EFileFolder? Folder { get; set; }
    public EFileType? Type { get; set; }
}
