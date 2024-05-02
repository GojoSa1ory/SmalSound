using System.Security.Claims;
using KPCourseWork.Dto.TrackDto;
using KPCourseWork.Service.TrackService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KPCourseWork.Controllers;

[ApiController]
[Route("[controller]")]
public class TrackController: ControllerBase
{
    private readonly ITrackService _service;
    
    public TrackController(ITrackService service)
    {
        _service = service;
    }
    
    [Authorize(Roles = "user, admin, artist")]
    [HttpPost("create")]
    public async Task<ActionResult<ServiceResponse<GetTrackDto>>> Create([FromForm] SetTrackDto track)
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        
        var response = await _service.UploadTrack(track, userId);

        if (!response.Success) return BadRequest(response);
        
        return Ok(response);
    }

    [HttpGet("All")]
    public async Task<ActionResult<ServiceResponse<List<GetTrackDto>>>> GetAllTracks()
    {
        var response = await _service.GetAllTracks();

        if (!response.Success) return BadRequest(response);

        return Ok(response);
    }
    
    [HttpGet("user/all")]
    public async Task<ActionResult<ServiceResponse<List<GetTrackDto>>>> GetAllUserTracks()
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        
        var response = await _service.GetAllTracks();

        if (!response.Success) return BadRequest(response);

        return Ok(response);
    }
    
    [HttpGet("track/{trackId}")]
    public async Task<ActionResult<ServiceResponse<GetTrackDto>>> GetOneTrack(int trackId)
    {
        var response = await _service.GetOneTrack(trackId);

        if (!response.Success) return BadRequest(response);

        return Ok(response);
    }

    [HttpGet("search/{request}")]
    public async Task<ActionResult<ServiceResponse<List<GetTrackDto>>>> SearchTrack(string request)
    {
        var response = await _service.SearchTrack(request);

        if (!response.Success) return BadRequest(response);
        
        return response;
    }
    
    [HttpGet("sort/{sortMethod}")]
    public async Task<ActionResult<ServiceResponse<List<GetTrackDto>>>> SortTrack(string sortMethod)
    {
        var response = await _service.SortTrack(sortMethod);

        if (!response.Success) return BadRequest(response);
        
        return response;
    }

    [HttpDelete("delete/{trackId}")]
    public async Task<ActionResult<ServiceResponse<string>>> DeleteTrack(int trackId)
    {
        var response = await _service.DeleteTrack(trackId);

        if (!response.Success) return BadRequest(response);
        
        return Ok(response);
    }
}