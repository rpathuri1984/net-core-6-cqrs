using Microsoft.Extensions.DependencyInjection;

namespace RYB.Cryptography;

public static class ServiceExtentions
{
    public static void AddRYBCryptography(this IServiceCollection services)
    {
        services.AddTransient<IJwtUtils, JwtUtils>();
    }
}

