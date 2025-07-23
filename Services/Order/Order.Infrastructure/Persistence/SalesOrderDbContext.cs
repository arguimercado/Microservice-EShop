

using Order.Application.Commons.Contracts;
using System.Reflection;

namespace Order.Infrastructure.Persistence;

public class SalesOrderDbContext : DbContext, IUnitWork
{
    public DbSet<SalesOrder> SalesOrders => Set<SalesOrder>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<SalesOrderItem> SalesOrderItems => Set<SalesOrderItem>();

    public SalesOrderDbContext(DbContextOptions<SalesOrderDbContext> opt) : base(opt)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);

    }

    public async Task<int> CommitSaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await SaveChangesAsync(cancellationToken);
    }
}
