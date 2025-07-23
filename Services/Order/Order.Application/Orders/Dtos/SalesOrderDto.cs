namespace Order.Application.Orders.Dtos;

public record SalesOrderDto(
    Guid CustomerId,
    string OrderName,
    AddressDto ShippingAddress,
    AddressDto BillingAddress,
    PaymentDto Payment,
    IEnumerable<OrderItemDto> OrderItems);



