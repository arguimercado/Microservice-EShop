namespace Order.Domain.ProductDomain.Types;

public record ProductId
{
    public static ProductId New() => new ProductId(Guid.NewGuid());
    protected ProductId(Guid value)
    {
        Value = value;
    }
    public Guid Value { get; }
}
