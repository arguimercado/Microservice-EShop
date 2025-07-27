namespace Order.Application.Orders.Queries;

public record GetSingleOrderQuery(Guid OrderId) : IQuery<SalesOrderDto>;


public class GetSingleOrderValidator : AbstractValidator<GetSingleOrderQuery> {

    public GetSingleOrderValidator()
    {
        //check guid if valid id like "23232"
        RuleFor(x => x.OrderId)
            .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("Order ID must be a valid GUID.")
            .NotNull().WithMessage("Order ID cannot be null.")
            .NotEmpty().WithMessage("Order ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Order ID cannot be empty.");

       

    }
}

internal class GetSingleOrderQueryHandler(ISalesOrderRepository repository) : IQueryHandler<GetSingleOrderQuery, SalesOrderDto>
{
    public async Task<Result<SalesOrderDto>> Handle(GetSingleOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await repository.GetOrderByIdAsync(
            id: SalesOrderId.Of(request.OrderId),
            option: new TrackingWithChildConfigureOption(false, true),
            cancellationToken: cancellationToken);

        if(order == null)
            return Result.Fail(new NotFoundErrorResult($"Order with ID {request.OrderId} not found."));

        var orderDto = SalesOrderMapper.MapToDto(order);

        return Result.Ok(orderDto);

    }
}
