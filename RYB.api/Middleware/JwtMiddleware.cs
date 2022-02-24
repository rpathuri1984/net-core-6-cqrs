using MediatR;
using Microsoft.Extensions.Options;
using RYB.Cryptography;
using RYB.MediatR.Requests;
using RYB.Model.AppSettings;

namespace RYB.api.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Jwt _jwtAppSettings;
    public JwtMiddleware(RequestDelegate next, IOptions<Jwt> appSettings)
    {
        _next = next;
        _jwtAppSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, IMediator mediatR, IJwtUtils jwtUtils)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        string userId = jwtUtils.ValidateJwtToken(token ?? "");
        if (!string.IsNullOrEmpty(userId))
        {
            // attach user to context on successful jwt validation
            context.Items["User"] = await mediatR.Send(new GetUserByTokenQuery(userId));
        }

        await _next(context);
    }
}

