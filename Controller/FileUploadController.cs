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
    
    [HttpGet]
    [HttpGet("file/get")]
    public async Task<IActionResult> GetImage(string path)
    {
        string filePath = $"./{path}";

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound("File not found");
        }

        byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

        string contentType = "application/octet-stream";

        string fileName = Path.GetFileName(filePath);

        return File(fileBytes, contentType, fileName);
    }
}