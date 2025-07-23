namespace Order.Domain.SalesOrderDomain.Types;

public record SalesOrderId
{
    public static SalesOrderId New() => new SalesOrderId(Guid.NewGuid());
    protected SalesOrderId(Guid value)
    {
        Value = value;
    }
    public Guid Value { get; }
}
