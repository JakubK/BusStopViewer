namespace BusStopViewer.Api.Data;

public class UserStop
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int StopId { get; set; }
}