using System.Security.Claims;
using KPCourseWork.Dto.FavoriteDto;
using KPCourseWork.Service.FavoriteService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace KPCourseWork.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class FavoriteController: ControllerBase
{
    private readonly IFavoriteService _service;

    public FavoriteController(IFavoriteService service)
    {
        _service = service;
    }

    [HttpGet("user")]
    public async Task<ActionResult<ServiceResponse<GetFavoriteDto>>> GetFavorite()
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        
        var response = await _service.GetFavorite(userId);

        if (!response.Success) return BadRequest(response);
        
        return Ok(response);
    } 
    
    [HttpPost("user/add/{trackId}")]
    public async Task<ActionResult<ServiceResponse<GetFavoriteDto>>> AddToFavorite(int trackId)
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        
        var response = await _service.AddToFavorite(userId, trackId);

        if (!response.Success) return BadRequest(response);
        
        return Ok(response);
    } 
}