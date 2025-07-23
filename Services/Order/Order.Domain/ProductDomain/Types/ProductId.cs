namespace Order.Domain.ProductDomain.Types;

public record ProductId
{
    public static ProductId New() => new ProductId(Guid.NewGuid());
    public static ProductId Of(Guid value) => new ProductId(value);
    protected ProductId(Guid value)
    {
        Value = value;
    }
    public Guid Value { get; }
}
