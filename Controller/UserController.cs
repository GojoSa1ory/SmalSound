using System.Security.Claims;
using KPCourseWork.Service;
using Microsoft.AspNetCore.Mvc;

namespace KPCourseWork.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController: ControllerBase
{
    private readonly IUserService _service;
    
    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpGet("profile")]
    public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetProfile()
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        var response = await _service.GetProfile(userId);

        if (!response.Success) return BadRequest(response);
        
        return response;
    }
}