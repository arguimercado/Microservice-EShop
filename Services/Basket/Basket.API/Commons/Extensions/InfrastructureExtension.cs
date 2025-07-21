using Basket.API.Baskets.Contracts;
using Basket.API.Baskets.Persistence;

namespace Basket.API.Commons.Extensions;

public static class InfrastructureExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMarten(opt =>
        {
            var connectionString = configuration.GetConnectionString("BasketConnection");
            if (string.IsNullOrEmpty(connectionString)) {
                throw new InvalidOperationException("The connection string 'BasketDbConnection' is not configured.");
            }
            opt.Connection(connectionString);

        }).UseLightweightSessions();

        services.AddScoped<IBasketRepository,BasketRepository>();
        services.Decorate<IBasketRepository, CachedBasketRepository>();
        services.AddStackExchangeRedisCache(opt =>
        {
            opt.Configuration = configuration.GetConnectionString("RedisConnection");
           
        });

        return services;
    }
}
