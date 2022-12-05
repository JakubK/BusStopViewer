using BusStopViewer.Api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BusStopViewer.Api.Services;

public interface ITristarClient
{
    Task<List<Stop>> GetAllStopsAsync();
}

public class TristarClient : ITristarClient
{
    private readonly HttpClient _httpClient;
    public TristarClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<List<Stop>> GetAllStopsAsync()
    {
        var response = await _httpClient.GetAsync("/dataset/c24aa637-3619-4dc2-a171-a23eec8f2172/resource/4c4025f0-01bf-41f7-a39f-d156d201b82b/download/stops.json");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var date = DateTime.Now.ToString("yyyy-MM-dd");

        JObject jObject = JObject.Parse(content);
        var subJson = jObject[date]!["stops"].ToString();
        return JsonConvert.DeserializeObject<List<Stop>>(subJson)!;
    }
}