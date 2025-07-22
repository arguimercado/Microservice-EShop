using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Discount.Grpc.Data.Persistence;

public class DiscountContext : DbContext
{
    public DbSet<Coupon> Coupons { get; set; } = default!;
    public DiscountContext(DbContextOptions<DiscountContext> option) 
        : base(option)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Coupon>(opt =>
        {
            opt.HasKey(c => c.Id);
            opt.Property(c => c.ProductName)
                .HasMaxLength(250)
                .IsRequired();
            
        });

       
    }




}
