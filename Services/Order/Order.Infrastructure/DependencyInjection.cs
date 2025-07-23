
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Commons.Contracts;
using Order.Application.Orders.Contracts;
using Order.Infrastructure.Orders.Repositories;

namespace Order.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddOrderInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {

        services.AddScoped<ISaveChangesInterceptor,AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DomainEventInterceptor>();
        
        services.AddDbContext<SalesOrderDbContext>((sp,opt) =>
        {
            
            var connectionString = configuration.GetConnectionString("OrderConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'OrderConnection' is not configured.");
            }
           
            opt.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            
            opt.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(SalesOrderDbContext).Assembly.FullName);
            });
        });

        services.AddScoped<IUnitWork>(opt => opt.GetRequiredService<SalesOrderDbContext>());
        services.AddScoped<ISalesOrderRepository, SalesOrderRepository>();

        return services;
    }
}
