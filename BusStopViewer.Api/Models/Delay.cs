namespace BusStopViewer.Api.Models;

public class Delay
{
    public string Headsign { get; set; }
    public string TheoreticalTime { get; set; }
    public string EstimatedTime { get; set; }
    public string RouteId { get; set; }
}