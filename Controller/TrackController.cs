using System.Security.Claims;
using KPCourseWork.Dto;
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
    
    [Authorize(Roles = "user, admin")]
    [HttpPost("create")]
    public async Task<ActionResult<ServiceResponse<GetTrackDto>>> Create([FromForm] SetTrackDto track)
    {
        Console.WriteLine(track.Name);
        Console.WriteLine(track.Track);
        Console.WriteLine(track.TrackImage);
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
    
    [HttpGet("user/all/{userId}")]
    public async Task<ActionResult<ServiceResponse<List<GetTrackDto>>>> GetAllUserTracks(int userId)
    {
        
        var response = await _service.GetAllUserTracks(userId);

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

    [Authorize]
    [HttpPatch("update/{trackId}")]
    public async Task<ActionResult<ServiceResponse<GetTrackDto>>> UpdateTrack([FromForm] UpdateTrackDto track, int trackId) {

        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        var response = await _service.UpdateTrack(track, trackId, userId);

        if(!response.Success) return BadRequest(response);

        return Ok(response);
    }

    [HttpGet("genres")]
    public async Task<ActionResult<ServiceResponse<List<GetGenreDto>>>> GetGenres()
    {
        var response = await _service.GetGenres();

        if (!response.Success) return BadRequest(response);
        
        return Ok(response);
    }
    
    [Authorize]
    [HttpDelete("delete/{trackId}")]
    public async Task<ActionResult<ServiceResponse<string>>> DeleteTrack(int trackId)
    {
        var response = await _service.DeleteTrack(trackId);

        if (!response.Success) return BadRequest(response);
        
        return Ok(response);
    }
}