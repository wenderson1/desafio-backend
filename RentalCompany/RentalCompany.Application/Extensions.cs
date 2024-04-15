using Microsoft.Extensions.DependencyInjection;
using RentalCompany.Application.Interfaces;
using RentalCompany.Application.Services;
using RentalCompany.Application.Subscriber;


namespace RentalCompany.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDeliveryManService, DeliveryManService>();
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }
        public static IServiceCollection AddSubscriber(this IServiceCollection services)
        {
            services.AddHostedService<MotorcyclesAvailablesSubscriber>();
            return services;
        }


    }
}
