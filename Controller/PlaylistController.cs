using System.Security.Claims;
using KPCourseWork.Dto.PlaylistDto;
using KPCourseWork.Service.PlaylistService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KPCourseWork.Controllers;

[ApiController]
[Route("[controller]")]
public class PlaylistController: ControllerBase
{

    private readonly IPlaylistService _playlistService; 
    
    public PlaylistController(IPlaylistService service)
    {
        _playlistService = service;
    }
    
    [Authorize]
    [HttpPost("create")]
    public async Task<ActionResult<ServiceResponse<GetPlaylistDto>>> CreatePlaylist(SetPlaylistDto newPlaylist)
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        
        var response = await _playlistService.CreatePlaylist(newPlaylist, userId);

        if (!response.Success) return BadRequest(response);
        
        return response;
    }

    [HttpGet("all")]
    public async Task<ActionResult<ServiceResponse<List<GetPlaylistDto>>>> GetPlaylists()
    {
        var response = await _playlistService.GetPlaylists();
        
        if (!response.Success) return BadRequest(response);
        
        return response;
    }

    [HttpGet("one/{playlistId}")]
    public async Task<ActionResult<ServiceResponse<GetPlaylistDto>>> GetPlaylist(int playlistId)
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var response = await _playlistService.GetPlaylist(playlistId, userId);
        
        if (!response.Success) return BadRequest(response);
        
        return response;
    }
    
    [Authorize]
    [HttpPatch("update/{playlistId}")]
    public async Task<ActionResult<ServiceResponse<GetPlaylistDto>>> UpdatePlaylist([FromForm] UpdatePlaylistDto playlist, int playlistId)
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var response = await _playlistService.UpdatePlaylist(playlist, playlistId, userId);
        
        if (!response.Success) return BadRequest(response);
        
        return response;
    }
    
    [Authorize]
    [HttpPost("addTrack/{playlistId}/{trackId}")]
    public async Task<ActionResult<ServiceResponse<GetPlaylistDto>>> AddTrack(int playlistId, int trackId)
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var response = await _playlistService.AddTrack(playlistId, userId, trackId);
        
        if (!response.Success) return BadRequest(response);
        
        return response;
    }
    
    [Authorize]
    [HttpDelete("removeTrack/{playlistId}/{trackId}")]
    public async Task<ActionResult<ServiceResponse<GetPlaylistDto>>> RemoveTrack(int playlistId, int trackId)
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var response = await _playlistService.RemoveTrack(playlistId, userId, trackId);
        
        if (!response.Success) return BadRequest(response);
        
        return response;
    }

    [Authorize]
    [HttpDelete("remove/{playlistId}")]
    public async Task<ActionResult<ServiceResponse<string>>> Remove(int playlistId)
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        
        var response = await _playlistService.RemovePlaylist(playlistId, userId);
        
        if (!response.Success) return BadRequest(response);
        
        return response;
    }
    
    
}