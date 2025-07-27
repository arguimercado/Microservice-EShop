
using Order.Domain.SalesOrderDomain.Events;

namespace Order.Application.Orders.EventHandlers;

public class OrderCreateEventHandler(ILogger<OrderCreateEventHandler> logger) 
    : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Order created event handler invoked for Order ID: {OrderId}", notification.Order.Id);
        return Task.CompletedTask;
    }
}
