using Basket.API.Baskets.Contracts;
using Basket.API.Baskets.Persistence;
using Discount.Grpc;

namespace Basket.API.Commons.Extensions;

public static class InfrastructureExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        //database marten
        services.AddMarten(opt =>
        {
            var connectionString = configuration.GetConnectionString("BasketConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("The connection string 'BasketDbConnection' is not configured.");
            }
            opt.Connection(connectionString);

        }).UseLightweightSessions();

        //redis
        services.AddStackExchangeRedisCache(opt =>
        {
            opt.Configuration = configuration.GetConnectionString("RedisConnection");

        });

        services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(opt =>
        {
            var discountUrl = configuration["GrpcSettings:DiscountUrl"];
            if (string.IsNullOrEmpty(discountUrl))
            {
                throw new InvalidOperationException("The 'GrpcSettings:DiscountUrl' configuration is not set.");
            }
            opt.Address = new Uri(discountUrl);
        });

        services.AddScoped<IBasketRepository, BasketRepository>();
        services.Decorate<IBasketRepository, CachedBasketRepository>();
        services.AddScoped<IDiscountService, DiscountService>();


        return services;
    }
}
