using Microsoft.EntityFrameworkCore;
using Microsoft.FeatureManagement;
using Order.Infrastructure.Persistence;
using Order.Infrastructure.Persistence.Extensions;

namespace Order.API.Commons.Extensions;

public static class DatabaseExtension
{
    public static async Task InitializeDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<SalesOrderDbContext>();
        

        await dbContext.Database.MigrateAsync();
        

        await SeedAsync(dbContext);
    }

    private static async Task SeedAsync(SalesOrderDbContext dbContext)
    {
        await SeedProductAsync(dbContext);
        await SeedCustomerAsync(dbContext);
        await SeedSalesOrderAsync(dbContext);
    }

    private static async Task SeedCustomerAsync(SalesOrderDbContext dbContext)
    {
        //seed customer first
        if(!await dbContext.Customers.AnyAsync())
        {   
            await dbContext.Customers.AddRangeAsync(InitialData.Customers);
            await dbContext.SaveChangesAsync();
        }   
    }

    private static async Task SeedProductAsync(SalesOrderDbContext dbContext)
    {
        //seed product first
        if (!await dbContext.Products.AnyAsync())
        {
            await dbContext.Products.AddRangeAsync(InitialData.Products);
            await dbContext.SaveChangesAsync();
        }
    }

    private static async Task SeedSalesOrderAsync(SalesOrderDbContext dbContext)
    {
        //seed sales order first
        if (!await dbContext.SalesOrders.AnyAsync())
        {
            await dbContext.SalesOrders.AddRangeAsync(InitialData.OrdersWithItems);
            await dbContext.SaveChangesAsync();
        }
    }




}
