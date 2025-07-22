using Discount.Grpc.Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data.Extensions
{
    public static class MigrationExtension
    {
        public static async Task<IApplicationBuilder> UseMigration(this IApplicationBuilder app) { 
            using var scope = app.ApplicationServices.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountContext>();
            await dbContext.Database.MigrateAsync();
            await SeedData.Seed(dbContext);
            return app;

        }
    }
}
