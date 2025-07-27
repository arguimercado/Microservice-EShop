using Order.Domain.CustomerDomain.Types;

namespace Order.Application.Orders.Queries;

public record GetOrdersByCustomerQuery(Guid CustomerId) : IQuery<IEnumerable<SalesOrderDto>>;

internal class GetOrdersByCustomerQueryHandler(ISalesOrderRepository salesOrderRepository) : IQueryHandler<GetOrdersByCustomerQuery, IEnumerable<SalesOrderDto>>
{
    public async Task<Result<IEnumerable<SalesOrderDto>>> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
    {
        var orders = await salesOrderRepository.GetOrdersByCustomerIdAsync(
            customerId: CustomerId.Of(request.CustomerId),
            cancellation: cancellationToken);
        
        
        if (orders == null || !orders.Any())
            return Result.Fail(new NotFoundErrorResult($"No orders found for customer with ID {request.CustomerId}."));
        
        var orderDtos = orders.Select(o => SalesOrderMapper.MapToDto(o));
        
        
        return Result.Ok(orderDtos);
    }
}


