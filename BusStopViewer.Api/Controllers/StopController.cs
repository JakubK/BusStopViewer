using BusStopViewer.Api.Filters;
using BusStopViewer.Api.Models;
using BusStopViewer.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BusStopViewer.Api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class StopController : ControllerBase
{
    private readonly IStopService _stopService;
    private readonly ITristarClient _tristarClient;
    private readonly IMemoryCache _memoryCache;
    
    public StopController(IStopService stopService, ITristarClient tristarClient, IMemoryCache memoryCache)
    {
        _stopService = stopService;
        _tristarClient = tristarClient;
        _memoryCache = memoryCache;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Stop>>> GetAllStops()
    {
        return await GetAllStopsAsync();
    }

    [HttpGet("{stopId}")]
    public async Task<ActionResult<List<Delay>>> GetStopDelays(string stopId)
    {
        var result = await _tristarClient.GetStopDelaysAsync(stopId);
        return Ok(result);
    }
    
    [HttpGet("assigned")]
    public async Task<ActionResult<List<Stop>>> GetAssignedStops()
    {
        int userId = int.Parse(HttpContext.Items["User"].ToString());
        var stopIds = _stopService.GetAssignedStops(userId);
        var stops = await GetAllStopsAsync();

        var result = stops
            .Where(x => stopIds.Contains(x.StopId))
            .ToList();
        
        return Ok(result);
    }

    [HttpPost("{stopid}")]
    public async Task<ActionResult<List<Stop>>> AssignStopToUser(int stopId)
    {
        int userId = int.Parse(HttpContext.Items["User"].ToString());
        var stops = await GetAllStopsAsync();
        var stop = stops.FirstOrDefault(x => x.StopId == stopId);
        
        if(stop != null)
            _stopService.AssignStop(userId, stop);
        return Ok();
    }
    
    [HttpDelete("{stopid}")]
    public async Task<ActionResult<List<Stop>>> RemoveStopFromUser(int stopId)
    {
        int userId = int.Parse(HttpContext.Items["User"].ToString());
        var stops = await GetAllStopsAsync();
        var stop = stops.FirstOrDefault(x => x.StopId == stopId);
        
        if(stop != null)
            _stopService.RemoveStop(userId, stop);
        return Ok();
    }

    private async Task<List<Stop>> GetAllStopsAsync()
    {
        return await _memoryCache.GetOrCreateAsync("stops", cacheEntry =>
        {
            cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
            return _tristarClient.GetAllStopsAsync();
        });
    }
}