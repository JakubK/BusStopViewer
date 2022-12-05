using System.IdentityModel.Tokens.Jwt;
using BusStopViewer.Api.Services;

namespace BusStopViewer.Api.Filters;

//  Read JWT header to validate it
//  And set UserId for AuthorizeAttribute filter
public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IJwtService _jwtService;

    public JwtMiddleware(RequestDelegate next, IJwtService jwtService)
    {
        _next = next;
        _jwtService = jwtService;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"]
            .FirstOrDefault()?
            .Split(" ")
            .Last();

        if (token != null)
            AttachUserToContext(context, token);
        await _next(context);
    }

    private void AttachUserToContext(HttpContext context, string token)
    {
        var isValid = _jwtService.ValidateToken(token);
        if (isValid)
        {
            var jwt = new JwtSecurityToken(token);
            var userId = int.Parse(jwt!.Claims.First(x => x.Type == "id").Value);

            //  attach user to context on successful jwt validation
            context.Items["User"] = userId;
        }
        else
            context.Response.StatusCode = 401;
    }
}