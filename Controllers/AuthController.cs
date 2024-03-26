using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService service;

    public AuthController(IAuthService service)
    {
        this.service = service;
    }


    [HttpPost("register")]
    public async Task<ActionResult<ServiceResponse<GetUserDto>>> Register(SetUserDto user)
    {

        var result = await this.service.Register(user);

        if (result.Success is false)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<ServiceResponse<GetUserDto>>> Login(SetUserDto user)
    {
        var result = await this.service.Login(user);

        if (result.Success is false)
            return BadRequest(result);

        return Ok(result);
    }

}
