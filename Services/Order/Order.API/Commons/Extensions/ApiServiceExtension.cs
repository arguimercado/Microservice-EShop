using BuildingBlocks.Commons.Exceptions;
using Carter;

namespace Order.API.Commons.Extensions;

public static class ApiServiceExtension
{
    public static IServiceCollection AddApiService(this IServiceCollection services)
    {
        services.AddCarter();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        services.AddOpenApi();
       
        return services;
    }

    public static WebApplication UseApiService(this WebApplication app)
    {
        app.UseExceptionHandler();
        app.MapCarter();

        return app;
    }
}


