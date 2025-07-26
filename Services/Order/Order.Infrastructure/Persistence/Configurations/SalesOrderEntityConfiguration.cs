using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Order.Infrastructure.Persistence.Configurations;

internal class SalesOrderEntityConfiguration : IEntityTypeConfiguration<SalesOrder>
{
    public void Configure(EntityTypeBuilder<SalesOrder> builder)
    {
        builder.HasKey(so => so.Id);
        builder.Property(so => so.Id)
            .HasConversion(
                orderId => orderId.Value,
                dbId => SalesOrderId.Of(dbId)
            );
        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(so => so.CustomerId)
            .IsRequired();

        builder.HasMany(so => so.SalesOrderItems)
            .WithOne(oi => oi.SalesOrder)
            .HasForeignKey(oi => oi.SalesOrderId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.ComplexProperty(so => so.ShippingAddress, ab => { 
            ab.Property(a => a.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            ab.Property(a => a.LastName)
                .HasMaxLength(50)
                .IsRequired();

            ab.Property(a => a.EmailAddress)
                .HasMaxLength(50);
            
            ab.Property(a => a.AddressLine)
                .HasMaxLength(200)
                .IsRequired();

            ab.Property(a => a.City)
                .HasMaxLength(50)
                .IsRequired();

            ab.Property(a => a.Country)
               .HasMaxLength(50);

            ab.Property(a => a.State)
                .HasMaxLength(50);

            ab.Property(a => a.ZipCode)
                .HasMaxLength(20);
        });


        builder.ComplexProperty(so => so.BillingAddress, ab => {
            ab.Property(a => a.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            ab.Property(a => a.LastName)
                .HasMaxLength(50)
                .IsRequired();

            ab.Property(a => a.EmailAddress)
                .HasMaxLength(50);

            ab.Property(a => a.AddressLine)
                .HasMaxLength(200)
                .IsRequired();

            ab.Property(a => a.City)
                .HasMaxLength(50)
                .IsRequired();

            ab.Property(a => a.Country)
               .HasMaxLength(50);

            ab.Property(a => a.State)
                .HasMaxLength(50);

            ab.Property(a => a.ZipCode)
                .HasMaxLength(20);
        });

        builder.ComplexProperty(p => p.Payment, pb =>
        {
            pb.Property(p => p.CardName)
                .HasMaxLength(50);

            pb.Property(p => p.CardNumber)
                .HasMaxLength(24)
                .IsRequired();

            pb.Property(p => p.CVV)
                 .HasColumnType("int")
                 .IsRequired();

            pb.Property(p => p.PaymentMethod)
                .HasColumnType("int")
                .IsRequired();

            pb.Property(p => p.Expiration)
                .HasColumnType("datetime")
                .IsRequired(false);
        });

        builder.Property(so => so.Status)
            .HasDefaultValue(OrderStatusEnum.Draft)
            .HasConversion(
                s => s.ToString(),
                dbStatus => (OrderStatusEnum)Enum.Parse(typeof(OrderStatusEnum), dbStatus));

        builder.Property(so => so.TotalPrice)
            .HasColumnType("decimal(8,2)");

    }
}

internal class SalesOrderItemConfiguration : IEntityTypeConfiguration<SalesOrderItem>
{
    public void Configure(EntityTypeBuilder<SalesOrderItem> builder)
    {
        builder.HasKey(i => i.Id);
        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(i => i.ProductId);

        builder.Property(i => i.Quantity).IsRequired();
        builder.Property(i => i.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
    }
}
