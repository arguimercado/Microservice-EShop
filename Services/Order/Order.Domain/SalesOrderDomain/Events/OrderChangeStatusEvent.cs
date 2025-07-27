using Order.Domain.SalesOrderDomain.Models;

namespace Order.Domain.SalesOrderDomain.Events;

public class OrderChangeStatusEvent : IDomainEvent
{
    public SalesOrder Order { get;  }
  

    public OrderChangeStatusEvent(SalesOrder order)
    {
        Order = order ?? throw new ArgumentNullException(nameof(order));

    }
}
