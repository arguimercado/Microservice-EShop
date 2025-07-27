using Order.Domain.CustomerDomain.Types;
using Order.Domain.ProductDomain.Types;
using Order.Domain.SalesOrderDomain.Events;
using Order.Domain.SalesOrderDomain.Types;

namespace Order.Domain.SalesOrderDomain.Models;

public class SalesOrder : AggregateRoot<SalesOrderId>
{
    public static SalesOrder Create(
        CustomerId customerId,
        string customerName,
        Address shippingAddress,
        Address billingAddress
    ) => new SalesOrder(SalesOrderId.New(),customerId, customerName, shippingAddress, billingAddress);

    protected SalesOrder() : base(SalesOrderId.New()) {

    }

    protected SalesOrder(SalesOrderId id, CustomerId customerId, string customerName, Address shippingAddress, Address billingAddress)
    : base(id)
    {
        CustomerId = customerId;
        CustomerName = customerName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = Payment.Empty();

        AddDomainEvent(new OrderCreatedEvent(this));
    }

    public CustomerId CustomerId { get; private set; } = default!;
    public string CustomerName { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;

    private decimal _totalPrice = 0;
    public decimal TotalPrice { 
        get  {
            if (_totalPrice == 0)
                return SalesOrderItems.Sum(item => item.Price * item.Quantity);
            else
                return _totalPrice;
        }
        private set {
            _totalPrice = value;
        } 
    } 

    public SalesOrder AddPayment(string cardName,string cardNumber, DateTime? expiration,int cvv,int method) {
        
        Payment = Payment.New(cardName,cardNumber,expiration,cvv,method);
        return this;
    }

    public OrderStatusEnum Status { get; private set; } = OrderStatusEnum.Pending;

    public void ChangeStatus(OrderStatusEnum status)
    {
        Status = status;
        AddDomainEvent(new OrderChangeStatusEvent(this));
    }

    private readonly List<SalesOrderItem> _salesOrderItems = new();
    public IReadOnlyCollection<SalesOrderItem> SalesOrderItems => _salesOrderItems.AsReadOnly();
    

    public SalesOrder Update(Address shipping,Address billing)
    {
        ShippingAddress = shipping ?? throw new ArgumentNullException(nameof(shipping));
        BillingAddress = billing ?? throw new ArgumentNullException(nameof(billing));
        
        
        return this;
    }

    public SalesOrder AddOrderItem(SalesOrderItem item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        _salesOrderItems.Add(item);
        return this;
    }

    public SalesOrder AddOrderItem(ProductId productId,string productName, int quantity, decimal price)
    {
        if (productId == null) throw new ArgumentNullException(nameof(productId));
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
        if (price <= 0) throw new ArgumentOutOfRangeException(nameof(price), "Price must be greater than zero.");
        
        var item = SalesOrderItem.Create(Id, productId,productName, quantity, price);
        return AddOrderItem(item);
        
    }

    public void RemoveItem(SalesOrderItem item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        _salesOrderItems.Remove(item);
    }


}
