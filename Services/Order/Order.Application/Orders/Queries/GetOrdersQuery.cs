using DomainBlocks.Commons.Options;
using Order.Application.Orders.Contracts;

namespace Order.Application.Orders.Queries;

public record GetOrdersQuery(PaginationRequest Request) : IQuery<IEnumerable<SalesOrderRequestDto>>;

internal class GetOrdersQueryHandler(ISalesOrderRepository salesOrderRepository) : IQueryHandler<GetOrdersQuery, IEnumerable<SalesOrderRequestDto>>
{
  
    public async Task<Result<IEnumerable<SalesOrderRequestDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await salesOrderRepository.GetOrdersAsync(request.Request, cancellationToken);
        var orderDtos = orders.Select(o => SalesOrderMapper.MapToDto(o));

        return Result.Ok(orderDtos);
    }
}



