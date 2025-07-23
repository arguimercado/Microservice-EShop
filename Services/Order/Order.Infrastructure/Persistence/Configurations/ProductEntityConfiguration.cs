using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Order.Infrastructure.Persistence.Configurations;

internal class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(
                productId => productId.Value,
                dbId => ProductId.Of(dbId)
            );


        builder.Property(p => p.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

    }
}
