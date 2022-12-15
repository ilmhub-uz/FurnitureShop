using Microsoft.AspNetCore.Components.Forms;

namespace FurnitureShop.Admin.Blazor.Dtos;

public class AddFiles
{
    public List<IBrowserFile>? Files { get; set; }
    public EFileFolder? Folder { get; set; }
    public EFileType? Type { get; set; }
}
