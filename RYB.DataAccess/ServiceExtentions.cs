using Microsoft.Extensions.DependencyInjection;

namespace RYB.DataAccess
{
    public static class ServiceExtentions
    {
        public static void AddRYBDataAccess(this IServiceCollection services)
        {
            services.AddTransient<IDbAccess, SqlDbAccess>();
        }
    }
}
