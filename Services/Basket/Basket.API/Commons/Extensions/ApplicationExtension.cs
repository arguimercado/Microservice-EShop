using BuildingBlocks.Commons.Behaviors;

namespace Catalog.Api.Commons.Extensions;

public static class ApplicationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        // Register MediatR
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(ApplicationExtension).Assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));

        });

        // Register FluentValidation
       services.AddValidatorsFromAssembly(typeof(ApplicationExtension).Assembly);
        return services;
    }
}
