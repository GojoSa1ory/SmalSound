using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace KPCourseWork.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController: ControllerBase
{
    private readonly IAuthService _service;
    
    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ServiceResponse<AuthDto>>> Register(SetUserDto user)
    {
        var response = await _service.Register(user);

        if (!response.Success) return BadRequest(response);
        
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<ServiceResponse<AuthDto>>> Login(SetAuthUserDto user)
    {
        var response = await _service.Login(user);

        if (!response.Success) return BadRequest(response);
        
        return Ok(response);
    }

    [HttpGet("verify")]
    [Authorize]
    public async Task<ActionResult<ServiceResponse<GetUserDto>>> VerifyUser()
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        var response = await _service.VerifyUser(userId);

        if (!response.Success) return BadRequest(response);

        return Ok(response);
    }

    [HttpPost("create/role")]
    public async Task<ActionResult<ServiceResponse<RoleModel>>> CreateRole(RoleModel role)
    {
        var response = await _service.CreateRole(role);

        return response;
    }
}