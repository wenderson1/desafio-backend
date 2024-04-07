using Microsoft.Extensions.DependencyInjection;
using User.Application.Interfaces;
using User.Application.Services;

namespace User.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
           services.AddScoped<IMotorcycleService, MotorcycleService>();

            return services;
        }
    }
}
