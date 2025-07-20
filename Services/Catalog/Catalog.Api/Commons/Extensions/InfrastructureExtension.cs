namespace Catalog.Api.Commons.Extensions
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMarten(opt =>
            {
                var connectionString = configuration.GetConnectionString("CatalogConnection");
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("The connection string 'ProductDbConnection' is not configured.");
                }
                opt.Connection(connectionString);
               
            }).UseLightweightSessions();

            return services;
        }
    }
}
