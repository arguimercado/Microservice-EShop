using Order.Domain.ProductDomain.Types;

namespace Order.Domain.ProductDomain.Models;

public class Product : Entity<ProductId>
{
    public static Product Create(string name, string description, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty.", nameof(name));
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description cannot be empty.", nameof(description));
        if (price <= 0) throw new ArgumentOutOfRangeException(nameof(price), "Price must be greater than zero.");
        
        return new Product(ProductId.New(), name, description, price);
    }

    protected Product() : base(ProductId.New())
    {
    }

    protected Product(ProductId id, string name, string description, decimal price) : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
    }
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;
}
