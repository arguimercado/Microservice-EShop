using BankCode.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BankCode.API.Infrastructures.Persistence;

public class BankDbContext : DbContext
{
    public BankDbContext(DbContextOptions<BankDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BankDbContext).Assembly);
    }
    public DbSet<BankProfile> BankProfiles => Set<BankProfile>();
}
