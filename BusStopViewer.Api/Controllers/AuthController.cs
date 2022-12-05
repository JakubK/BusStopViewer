using BusStopViewer.Api.Request;
using BusStopViewer.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusStopViewer.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public ActionResult Register([FromBody] RegisterRequest request)
    {
        _authService.Register(request.Email, request.Password);
        return Ok();
    }

    [HttpPost("login")]
    public ActionResult Login([FromBody] LoginRequest request)
    {
        return Ok(_authService.Login(request.Email, request.Password));
    }
}