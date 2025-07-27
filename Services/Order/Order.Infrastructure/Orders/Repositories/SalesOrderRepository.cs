using DomainBlocks.Commons.Options;
using Order.Application.Orders.Contracts;

namespace Order.Infrastructure.Orders.Repositories;

internal class SalesOrderRepository(SalesOrderDbContext context) : ISalesOrderRepository
{

    public void Add(SalesOrder salesOrder)
    {
        context.SalesOrders.Add(salesOrder);
    }

    public async Task<IEnumerable<SalesOrder>> GetOrdersAsync(PaginationRequest pagination,CancellationToken cancellationToken)
    {
        return await context.SalesOrders
            .Include(o => o.SalesOrderItems)
            .Take(pagination.PageSize)
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .ToListAsync(cancellationToken);
    }

    public void Update(SalesOrder salesOrder)
    {
        context.SalesOrders.Update(salesOrder);
    }
}
