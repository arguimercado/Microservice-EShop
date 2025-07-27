using DomainBlocks.Commons.Options;
using Order.Application.Orders.Contracts;

namespace Order.Infrastructure.Orders.Repositories;

internal class SalesOrderRepository(SalesOrderDbContext context) : ISalesOrderRepository
{

    public void Add(SalesOrder salesOrder)
    {
        context.SalesOrders.Add(salesOrder);
    }

    public void Update(SalesOrder salesOrder)
    {
        context.SalesOrders.Update(salesOrder);
    }
    public Task<SalesOrder?> GetOrderByIdAsync(SalesOrderId id, TrackingWithChildConfigureOption option, CancellationToken cancellationToken = default)
    {
        var query = context.SalesOrders.AsQueryable();
        if (option.AsTracking) {
            query = query.AsTracking();
        }

        if (option.IncludeChildren) {
            query = query.Include(o => o.SalesOrderItems);
        }

        return query.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<SalesOrder>> GetOrdersAsync(PaginationRequest pagination,CancellationToken cancellationToken = default)
    {
        return await context.SalesOrders
            .Include(o => o.SalesOrderItems)
            .Take(pagination.PageSize)
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<SalesOrder>> GetOrdersByCustomerIdAsync(CustomerId customerId, PaginationRequest pagination, CancellationToken cancellation = default)
    {
        var customerOrders = await context.SalesOrders.Where(o => o.CustomerId == customerId)
            .Take(pagination.PageSize)
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .ToListAsync(cancellation);

        return customerOrders;
    }

    public async Task<IEnumerable<SalesOrder>> GetOrdersByCustomerIdAsync(CustomerId customerId, CancellationToken cancellation = default)
    {
        var customerOrders = await context.SalesOrders
            .Where(o => o.CustomerId == customerId)
            .ToListAsync(cancellation);

        return customerOrders;
    }

    
}
