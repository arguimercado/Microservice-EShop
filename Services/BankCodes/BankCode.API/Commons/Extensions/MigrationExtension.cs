using BankCode.API.Infrastructures.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BankCode.API.Commons.Extensions;

public static class MigrationExtension
{
    public static async Task<IApplicationBuilder> UseMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<BankDbContext>();
        await dbContext.Database.MigrateAsync();
        
        return app;

    }
}
