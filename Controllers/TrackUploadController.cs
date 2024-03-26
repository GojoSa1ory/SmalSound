using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers;

[ApiController]
[Route("api/file/[controller]")]
public class TrackUploadController : ControllerBase
{

    private ITrackUploadService service;

    public TrackUploadController(ITrackUploadService service)
    {
        this.service = service;
    }

    [HttpPost("add")]
    public async Task<ActionResult<ServiceResponse<GetTrackDto>>> PostFile([FromForm] SetTrackDto track)
    {

        var result = await this.service.PostTrack(track);

        if (result.Data is null)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<GetTrackDto>>> GetImage(string path)
    {
        string filePath = $"./{path}";

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound("File not found");
        }

        byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

        string contentType = "application/octet-stream"; // Пример MIME-типа для общих файлов

        string fileName = Path.GetFileName(filePath);

        return File(fileBytes, contentType, fileName);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetTrackDto>>> GetOneTrack(int id)
    {
        var response = await this.service.GetTrack(id);

        if (response.Data is null)
            return NotFound(response);

        return response;
    }

    // [HttpGet("image/{path}")]
    // public IActionResult GetImage(string path)
    // {
    //     string filePath = $"./{path}";
    //     if (!System.IO.File.Exists(filePath))
    //     {
    //         return NotFound("Image not found");
    //     }
    //     var imageBytes = System.IO.File.ReadAllBytes(filePath);
    //     return File(imageBytes, "image/jpeg");
    // }

    // [HttpGet("track/{fileName}")]
    // public IActionResult GetTrack(string fileName)
    // {
    //     var filePath = Path.Combine(_trackFolderPath, fileName);
    //     if (!System.IO.File.Exists(filePath))
    //     {
    //         return NotFound("Track not found");
    //     }
    //     var trackBytes = System.IO.File.ReadAllBytes(filePath);
    //     return File(trackBytes, "audio/mpeg");
    // }

}
