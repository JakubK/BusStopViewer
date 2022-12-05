using BusStopViewer.Api.Data;
using BusStopViewer.Api.Models;

namespace BusStopViewer.Api.Services;

public interface IStopService
{
    void AssignStop(int userId, Stop stop);
    void RemoveStop(int userId, Stop stop);

    List<int> GetAssignedStops(int userId);
}

public class StopService : IStopService
{
    private readonly AppDbContext _dbContext;
    public StopService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AssignStop(int userId, Stop stop)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.Id == userId);
        if (user != null)
        {
            if (user.UserStops.All(x => x.StopId != stop.StopId)) // If user does not have this stop assigned
            {
                var userStop = new UserStop
                {
                    StopId = stop.StopId,
                    UserId = userId,
                };
                _dbContext.UserStops.Add(userStop);
                _dbContext.SaveChanges();
            }
        }
    }

    public void RemoveStop(int userId, Stop stop)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.Id == userId);
        if (user != null)
        {
            if (user.UserStops.All(x => x.StopId != stop.StopId)) // If user have this stop assigned
            {
                var userStop = _dbContext.UserStops
                    .FirstOrDefault(x => x.UserId == userId && x.StopId == stop.StopId);
                _dbContext.UserStops.Remove(userStop);
                _dbContext.SaveChanges();
            }
        }
    }

    public List<int> GetAssignedStops(int userId)
    {
        return _dbContext.Users
            .FirstOrDefault(x => x.Id == userId)
            .UserStops
            .Select(x => x.StopId)
            .ToList();
    }
}