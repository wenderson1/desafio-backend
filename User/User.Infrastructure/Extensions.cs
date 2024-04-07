using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using User.Core.Repositories;
using User.Infrastructure.Persistence;
using User.Infrastructure.Persistence.Repositories;

namespace User.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                var configuration = sp.GetService<IConfiguration>();

                var options = new MongoDbOptions();
                configuration.GetSection("Mongo").Bind(options);

                return options;
            });

            services.AddSingleton(sp =>
            {
                var options = sp.GetService<MongoDbOptions>();

                return new MongoClient(options.ConnectionString);
            });

            services.AddTransient(sp =>
            {
                BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

                var options = sp.GetService<MongoDbOptions>();
                var mongoClient = sp.GetService<MongoClient>();

                return mongoClient.GetDatabase(options.Database);
            });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
            services.AddScoped<IHistoricRepository, HistoricRepository>();
            services.AddScoped<IDeliveryManRepository, DeliveryManRepository>();

            return services;
        }

    }
}
