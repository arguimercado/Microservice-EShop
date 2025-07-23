using Order.Domain.SalesOrderDomain.Models;

namespace Order.Domain.SalesOrderDomain.Events;

public class OrderCreatedEvent : IDomainEvent
{
    public SalesOrder Order { get;  }

    public OrderCreatedEvent(SalesOrder order)
    {
        Order = order;
    }
}
