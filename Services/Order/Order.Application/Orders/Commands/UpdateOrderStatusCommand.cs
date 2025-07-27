namespace Order.Application.Orders.Commands;

public record UpdateOrderStatusCommand(Guid OrderId, SalesOrderRequestUpdateDto Request) : ICommand;

internal class UpdateOrderStatusCommandHandler(ISalesOrderRepository salesOrderRepository,IUnitWork unitWork) : ICommandHandler<UpdateOrderStatusCommand>
{
    public async Task<Result<Unit>> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await salesOrderRepository.GetOrderByIdAsync(
            id: SalesOrderId.Of(request.OrderId),
            option: new TrackingWithChildConfigureOption(true, false),
            cancellationToken: cancellationToken);

        if (order is null)
            return Result.Fail(new NotFoundErrorResult($"Order Id: {request.OrderId} not found"));

        order.ChangeStatus(Enum.Parse<OrderStatusEnum>(request.Request.OrderStatus));

        salesOrderRepository.Update(order);
        await unitWork.CommitChangesAsync(cancellationToken);

        return Result.Ok(Unit.Value);

    }
}
