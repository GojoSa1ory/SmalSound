using KPCourseWork.Dto;
using KPCourseWork.Service;
using Microsoft.AspNetCore.Mvc;

namespace KPCourseWork.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController: ControllerBase
{
    private readonly ISearchService _service;

    public SearchController(ISearchService service)
    {
        _service = service;
    }

    [HttpGet("search/{request}")]
    public async Task<ActionResult<ServiceResponse<SearchResponseDto>>> Search(string request)
    {
        var response = await _service.Search(request);

        if (!response.Success) return BadRequest(response);
        
        return Ok(response);
    }
    
    [HttpGet("sort/{sortMethod}")]
    public async Task<ActionResult<ServiceResponse<SearchResponseDto>>> SortTrack(string sortMethod)
    {
        var response = await _service.SortTrack(sortMethod);

        if (!response.Success) return BadRequest(response);
        
        return response;
    }

    [HttpGet("filter/{filterId}")]
    public async Task<ActionResult<ServiceResponse<SearchResponseDto>>> FilterTrack(int filterId)
    {
        var response = await _service.FilterTrack(filterId);

        if (!response.Success) return BadRequest(response);
        
        return Ok(response);
    }
}