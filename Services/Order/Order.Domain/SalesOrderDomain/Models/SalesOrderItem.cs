using Order.Domain.ProductDomain.Types;
using Order.Domain.SalesOrderDomain.Types;

namespace Order.Domain.SalesOrderDomain.Models;

public class SalesOrderItem : Entity<Guid>
{

    public static SalesOrderItem Create(SalesOrderId orderId, ProductId productId, int quantity, decimal price)
        => new SalesOrderItem(Guid.NewGuid(), orderId, productId, quantity, price);


    //use in Entity Framework
    protected SalesOrderItem() : base(Guid.NewGuid()) {}
    
    
    protected SalesOrderItem(Guid id, SalesOrderId orderId, ProductId productId, int quantity, decimal price) 
        : base(id) {   
        
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
        
    }

    public SalesOrderId OrderId { get; private set; } = default!;

    public ProductId ProductId { get; private set; } = default!;

    public int Quantity { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;
    public virtual SalesOrder SalesOrder { get; set; } = default!;

}
