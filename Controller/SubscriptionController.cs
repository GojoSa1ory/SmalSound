using System.Security.Claims;
using KPCourseWork.Data;
using KPCourseWork.Service.SubscriptionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KPCourseWork.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class SubscriptionController: ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionController(ISubscriptionService service)
    {
        _subscriptionService = service;
    }
    
    [HttpPost("subscription/subscribe/{subscribedToId}")]
    public async Task<IActionResult> Subscribe(int subscribedToId)
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value); 
        
        var response = await _subscriptionService.Subscribe(userId, subscribedToId);

        if (!response.Success)
        {
            return BadRequest(response.Message);
        }

        return Ok(response.Data);
    }

    [HttpPost("subscription/unsubscribe/{subscribedToId}")]
    public async Task<IActionResult> Unsubscribe(int subscribedToId)
    {
        
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value); 
        
        var response = await _subscriptionService.Unsubscribe(userId, subscribedToId);

        if (!response.Success)
        {
            return BadRequest(response.Message);
        }

        return Ok(response.Data);
    }

    // [HttpGet("subscription/subscribers")]
    // public async Task<IActionResult> GetSubscribers()
    // {
    //     
    //     int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value); 
    //     
    //     var response = await _subscriptionService.GetSubscribers(userId);
    //
    //     if (!response.Success)
    //     {
    //         return BadRequest(response.Message);
    //     }
    //
    //     return Ok(response.Data);
    // }
    
    [HttpGet("{userId}/subscriptions")]
    public async Task<IActionResult> GetSubscriptions(int userId)
    {
        var response = await _subscriptionService.GetSubscriptions(userId);
    
        if (!response.Success)
        {
            return BadRequest(response.Message);
        }
    
        return Ok(response.Data);
    }
    

}