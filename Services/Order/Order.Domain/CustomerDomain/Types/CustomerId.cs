namespace Order.Domain.CustomerDomain.Types;

public record CustomerId
{
    public static CustomerId New() => new CustomerId(Guid.NewGuid());
    protected CustomerId(Guid value)
    {
        Value = value;
    }
    public Guid Value { get; }
}
