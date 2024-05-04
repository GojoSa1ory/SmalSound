using KPCourseWork.Service;
using Microsoft.AspNetCore.Mvc;

namespace KPCourseWork.Controllers;

[ApiController]
[Route("[controller]")]
public class FileUploadController: ControllerBase
{
    private readonly IFileUploadService _service;

    public FileUploadController(IFileUploadService service)
    {
        _service = service;
    }

    // [HttpGet("file/get")]
    // public async Task<IActionResult> GetImage(string path)
    // {
    //     string filePath = $"./{path}";

    //     if (!System.IO.File.Exists(filePath))
    //     {
    //         return NotFound("File not found");
    //     }

    //     byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);

    //     string contentType = "audio/mpeg";

    //     string fileName = Path.GetFileName(filePath);

    //     return File(fileBytes, contentType, fileName);
    // }

    [HttpGet("file/get")]
    public IActionResult GetAudio(string path)
    {
        string filePath = $"./{path}";

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound("File not found");
        }

        var fileStream = System.IO.File.OpenRead(filePath);

        // Включаем поддержку Range requests
        HttpContext.Response.Headers.Add("Accept-Ranges", "bytes");

        // Возвращаем FileStream прямо из метода
        return File(fileStream, "audio/mpeg", Path.GetFileName(filePath));
    }

}
