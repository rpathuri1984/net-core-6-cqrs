using Microsoft.Extensions.DependencyInjection;
using RYB.Business;
using MediatR;
using System.Reflection;

namespace RYB.MediatR
{
    public static class ServiceExtentions
    {
        public static void AddRYBMediatR(this IServiceCollection services)
        {
            services.AddRYBBusiness();
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
