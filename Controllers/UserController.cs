using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace CourseWork.Controllers;

[Authorize]
[ApiController]
[Route("api/users/[controller]")]
public class UserController : ControllerBase
{

    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<GetUserDto>>> GetUsers()
    {
        var response = await this.userService.GetUsers();

        if (response.Data is null)
            return NotFound(response);

        return Ok(response);
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<GetUserDto>> GetUserByName(string name)
    {
        var response = await this.userService.GetUserByName(name);

        if (response.Data is null)
            return NotFound(response);

        return Ok(response);
    }

    [HttpDelete("delete")]
    public async Task<ActionResult<List<GetUserDto>>> RemoveUser(string name)
    {
        var response = await this.userService.RemoveUser(name);

        if (response.Data is null)
            return NotFound(response);

        return Ok(response);
    }

    [HttpPut("update")]
    public async Task<ActionResult<GetUserDto>> UpdateUser(SetUserDto updatedUser)
    {
        var response = await this.userService.UpdateUser(updatedUser);

        if (response.Data is null)
            return NotFound(response);

        return Ok(response);
    }

    // [HttpPost("addUser")]
    // public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> PostUser(SetUserDto newUser)
    // {
    //     var response = await this.userService.PostUser(newUser);

    //     if (response.Data is null)
    //         return BadRequest(response);

    //     return Ok(response);
    // }

}
