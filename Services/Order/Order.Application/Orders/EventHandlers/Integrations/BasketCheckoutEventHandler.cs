using BuildingBlocks.Messaging.Basket.Events;
using MassTransit;
using Order.Application.Orders.Commands;

namespace Order.Application.Orders.EventHandlers.Integrations;

public class BasketCheckoutEventHandler(ISender sender,ILogger<BasketCheckoutEventHandler> logger) : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        logger.LogInformation("BasketCheckoutEvent received: {@Event}", context.Message.GetType().Name);

        var command = new CreateOrderCommand(SalesOrderMapper.MapEventToRequest(context.Message));
        await sender.Send(command, context.CancellationToken);
    }
}
