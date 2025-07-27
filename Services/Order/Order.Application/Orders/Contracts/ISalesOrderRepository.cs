using Order.Domain.CustomerDomain.Types;
using Order.Domain.SalesOrderDomain.Models;

namespace Order.Application.Orders.Contracts;

public interface ISalesOrderRepository
{
    void Add(SalesOrder salesOrder);
    void Update(SalesOrder salesOrder);
    Task<SalesOrder?> GetOrderByIdAsync(SalesOrderId id,TrackingWithChildConfigureOption option, CancellationToken cancellationToken = default);
    Task<IEnumerable<SalesOrder>> GetOrdersAsync(PaginationRequest pagination, CancellationToken cancellationToken =default);

    Task<IEnumerable<SalesOrder>> GetOrdersByCustomerIdAsync(CustomerId customerId, PaginationRequest pagination, CancellationToken cancellation = default);

    Task<IEnumerable<SalesOrder>> GetOrdersByCustomerIdAsync(CustomerId customerId, CancellationToken cancellation = default);
}
