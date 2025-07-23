using Order.Application.Orders.Contracts;

namespace Order.Infrastructure.Orders.Repositories;

internal class SalesOrderRepository(SalesOrderDbContext context) : ISalesOrderRepository
{

    public void Add(SalesOrder salesOrder)
    {
        context.SalesOrders.Add(salesOrder);
    }

    public Task<IEnumerable<SalesOrder>> GetOrdersAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Update(SalesOrder salesOrder)
    {
        context.SalesOrders.Update(salesOrder);
    }
}
