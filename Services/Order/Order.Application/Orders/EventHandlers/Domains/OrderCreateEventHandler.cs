using MassTransit;
using Microsoft.FeatureManagement;
using Order.Domain.SalesOrderDomain.Events;

namespace Order.Application.Orders.EventHandlers.Domains;

public class OrderCreateEventHandler(
    IPublishEndpoint publishEndpoint, 
    IFeatureManager featureManager,
    ILogger<OrderCreateEventHandler> logger) 
    : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Order created event handler invoked for Order ID: {OrderId}", notification.Order.Id);
        
        if(await featureManager.IsEnabledAsync("OrderFullfilment")) {
            var orderCreatedIntegrationEvent = SalesOrderMapper.MapToDto(notification.Order);
            await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
            logger.LogInformation("Order created integration event published for Order ID: {OrderId}", notification.Order.Id);
        }
    }
}
