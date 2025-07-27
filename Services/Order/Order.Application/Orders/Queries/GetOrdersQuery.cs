namespace Order.Application.Orders.Queries;

public record GetOrdersQuery(PaginationRequest Request) : IQuery<IEnumerable<SalesOrderDto>>;

internal class GetOrdersQueryHandler(ISalesOrderRepository salesOrderRepository) : IQueryHandler<GetOrdersQuery, IEnumerable<SalesOrderDto>>
{
    public async Task<Result<IEnumerable<SalesOrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await salesOrderRepository.GetOrdersAsync(request.Request, cancellationToken);
        IEnumerable<SalesOrderDto> orderDtos = orders.Select(o => SalesOrderMapper.MapToDto(o));

        return Result.Ok(orderDtos);
    }
}



