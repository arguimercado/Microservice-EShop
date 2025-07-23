namespace Order.Domain.CustomerDomain.Types;

public record CustomerId
{
    public static CustomerId New() => new CustomerId(Guid.NewGuid());

    public static CustomerId Of(Guid value) => new CustomerId(value);
    protected CustomerId(Guid value)
    {
        Value = value;
    }
    public Guid Value { get; }
}
