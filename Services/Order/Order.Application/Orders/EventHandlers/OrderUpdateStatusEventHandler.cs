using Order.Domain.SalesOrderDomain.Events;

namespace Order.Application.Orders.EventHandlers;

public class OrderUpdateStatusEventHandler : INotificationHandler<OrderChangeStatusEvent>
{
    public Task Handle(OrderChangeStatusEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
