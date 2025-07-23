using Order.Domain.SalesOrderDomain.Models;

namespace Order.Domain.SalesOrderDomain.Events;

public class OrderAddressUpdatedEvent : IDomainEvent
{
    public SalesOrder Order { get;  }
    public Address Billing { get;  } 
    public Address Shipping { get;  }

    public OrderAddressUpdatedEvent(SalesOrder order,Address billing,Address shipping)
    {
        Order = order ?? throw new ArgumentNullException(nameof(order));
        Billing = billing ?? throw new ArgumentNullException(nameof(billing));
        Shipping = shipping ?? throw new ArgumentNullException(nameof(shipping));

    }
}
