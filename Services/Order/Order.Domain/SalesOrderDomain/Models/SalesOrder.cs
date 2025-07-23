using Order.Domain.CustomerDomain.Types;
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
    ) => new SalesOrder(SalesOrderId.New(), customerId, customerName, shippingAddress, billingAddress);

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

    public void AddPayment(string cardName,string cardNumber, DateTime expiration,int cvv,int method) {
        
        Payment = Payment.New(cardName,cardNumber,expiration,cvv,method);
    }

    public OrderStatusEnum Status { get; private set; } = OrderStatusEnum.Pending;

    public void ChangeStatus(OrderStatusEnum status) => Status = status;

    private readonly List<SalesOrderItem> _salesOrderItems = new();
    public IReadOnlyCollection<SalesOrderItem> SalesOrderItems => _salesOrderItems.AsReadOnly();
    public decimal TotalPrice => SalesOrderItems.Sum(x => x.Price * x.Quantity);

    public void Update(Address shipping,Address billing)
    {
        ShippingAddress = shipping ?? throw new ArgumentNullException(nameof(shipping));
        BillingAddress = billing ?? throw new ArgumentNullException(nameof(billing));
        
        AddDomainEvent(new OrderAddressUpdatedEvent(this, shipping, billing));
    }

    public void AddOrderItem(SalesOrderItem item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        _salesOrderItems.Add(item);
    }

    public void RemoveItem(SalesOrderItem item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        _salesOrderItems.Remove(item);
    }


}
