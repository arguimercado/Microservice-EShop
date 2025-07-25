﻿using Order.Domain.SalesOrderDomain.Models;

namespace Order.Application.Orders.Contracts;

public interface ISalesOrderRepository
{
    void Add(SalesOrder salesOrder);
    void Update(SalesOrder salesOrder);

    Task<IEnumerable<SalesOrder>> GetOrdersAsync(CancellationToken cancellationToken);
}
