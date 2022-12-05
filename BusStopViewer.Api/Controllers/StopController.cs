using BusStopViewer.Api.Models;
using BusStopViewer.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BusStopViewer.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class StopController : ControllerBase
{
    private readonly ITristarClient _tristarClient;
    private readonly IMemoryCache _memoryCache;
    
    public StopController(ITristarClient tristarClient, IMemoryCache memoryCache)
    {
        _tristarClient = tristarClient;
        _memoryCache = memoryCache;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Stop>>> GetAllStopsAsync()
    {
        return await _memoryCache.GetOrCreateAsync("stops", cacheEntry =>
        {
            cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
            return _tristarClient.GetAllStopsAsync();
        });
    }
}