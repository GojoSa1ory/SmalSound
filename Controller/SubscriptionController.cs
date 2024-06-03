using System.Security.Claims;
using KPCourseWork.Data;
using KPCourseWork.Service.SubscriptionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KPCourseWork.Controllers;

[ApiController]
[Route("[controller]")]
public class SubscriptionController: ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionController(ISubscriptionService service)
    {
        _subscriptionService = service;
    }
    
    [HttpPost("subscribe/{subscribedToId}")]
    [Authorize]
    public async Task<IActionResult> Subscribe(int subscribedToId)
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value); 
        
        var response = await _subscriptionService.Subscribe(userId, subscribedToId);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost("unsubscribe/{subscribedToId}")]
    [Authorize]
    public async Task<IActionResult> Unsubscribe(int subscribedToId)
    {
        
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value); 
        
        var response = await _subscriptionService.Unsubscribe(userId, subscribedToId);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpGet("check/{subscribedToId}")]
    [Authorize]
    public async Task<ActionResult<ServiceResponse<bool>>> CheckSubscription(int subscribedToId)
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value); 
        
        var response = await _subscriptionService.CheckSubscription(userId, subscribedToId);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
    
    [HttpGet("listenersCount/{userId}")]
    public async Task<ActionResult<ServiceResponse<int>>> GetSubscribersCount(int userId)
    {
        var response = await _subscriptionService.GetSubscribersCount(userId);

        if (!response.Success) return BadRequest(response);

        return response;
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