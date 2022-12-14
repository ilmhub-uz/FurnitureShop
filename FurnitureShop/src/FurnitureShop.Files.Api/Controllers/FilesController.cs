using FurnitureShop.Files.Api.Dtos;
using FurnitureShop.Files.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Files.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
    private IFileService _fileService;

    public FilesController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(FilesView), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddFiles([FromBody] AddFiles dto)
    {
        var saveFile = await _fileService.AddFilesAsync(dto);
        if (saveFile is null)
            return BadRequest();

        return Ok(saveFile);
    }

}
