using BuildingBlocks.Commons.Behaviors;
using BuildingBlocks.Messaging.MassTransits;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using System.Reflection;

namespace Order.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddOrderApplication(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        // Register FluentValidation
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddFeatureManagement(configuration.GetSection("FeatureFlags"));
        services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());
        
        return services;
    }
}
