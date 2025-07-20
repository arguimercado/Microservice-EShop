namespace Catalog.Api.Commons.Extensions
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            // Register MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationExtension).Assembly));
            return services;
        }
    }
}
