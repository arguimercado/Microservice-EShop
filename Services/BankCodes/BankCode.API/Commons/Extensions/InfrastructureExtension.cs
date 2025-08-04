using BankCode.API.Infrastructures.Persistence;
using DomainBlocks.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BankCode.API.Commons.Extensions;

public static class InfrastructureExtension
{
    public static IServiceCollection AddInfraService(this IServiceCollection services, IConfiguration configuration)
    {
        // Add your infrastructure services here
        // For example, adding DbContext, repositories, etc.
        // services.AddDbContext<BankDbContext>(options => ...);
        // services.AddScoped<IBankRepository, BankRepository>();
        services.AddScoped<SaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddDbContext<BankDbContext>((sp,opt) =>
        {
            opt.UseSqlite(configuration.GetConnectionString("BankDbConnection") ?? "Data Source=bankcodes.db");
            opt.AddInterceptors(sp.GetService<SaveChangesInterceptor>()!);
        });


        return services;
    }
}