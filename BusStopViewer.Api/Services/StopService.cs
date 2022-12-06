using BusStopViewer.Api.Data;
using BusStopViewer.Api.Models;
using Microsoft.EntityFrameworkCore;

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
        var user = _dbContext.Users.Include(x => x.UserStops).FirstOrDefault(x => x.Id == userId);
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
        var user = _dbContext.Users
            .Include(x => x.UserStops)
            .FirstOrDefault(x => x.Id == userId);
        if (user != null)
        {
            var userStop = _dbContext.UserStops.FirstOrDefault(x => x.StopId == stop.StopId);
            if (userStop != null)
            {
                _dbContext.UserStops.Remove(userStop);
                _dbContext.SaveChanges();
            }
        }
    }

    public List<int> GetAssignedStops(int userId)
    {
        return _dbContext.Users
            .Include(x => x.UserStops)
            .FirstOrDefault(x => x.Id == userId)
            .UserStops
            .Select(x => x.StopId)
            .ToList();
    }
}