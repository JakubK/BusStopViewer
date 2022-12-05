using BusStopViewer.Api.Data;

namespace BusStopViewer.Api.Services;

public interface IAuthService
{
    void Register(string email, string password);
    string Login(string email, string password);
}

public class AuthService : IAuthService
{
    private readonly AppDbContext _dbContext;
    private readonly IJwtService _jwtService;
    public AuthService(AppDbContext dbContext, IJwtService jwtService)
    {
        _dbContext = dbContext;
        _jwtService = jwtService;
    }
    
    public void Register(string email, string password)
    {
        if (!_dbContext.Users.Any(x => x.Email.ToLower() == email.ToLower()))
        {
            _dbContext.Users.Add(new User
            {
                Email = email.ToLower(),
                Password = password
            });
            _dbContext.SaveChanges();
        }
    }

    public string Login(string email, string password)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.Email == email.ToLower() && x.Password == password);
        if (user != null) //  Generate JWT
            return _jwtService.GenerateToken(user);
        return null;
    }
}