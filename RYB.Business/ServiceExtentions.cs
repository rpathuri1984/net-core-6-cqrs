using Microsoft.Extensions.DependencyInjection;
using RYB.DataAccess;

namespace RYB.Business
{
    public static class ServiceExtentions
    {
        public static void AddRYBBusiness(this IServiceCollection services)
        {
            services.AddRYBDataAccess();
            services.AddTransient<IUserRepo, UserRepo>();
        }
    }
}
