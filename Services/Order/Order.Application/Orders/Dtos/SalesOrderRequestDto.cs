using Order.Domain.SalesOrderDomain.Models;

namespace Order.Application.Orders.Dtos;

public static class SalesOrderMapper
{
    public static SalesOrderDto MapToDto(SalesOrder salesOrder)
    {

        return new SalesOrderDto(
            OrderId: salesOrder.Id.Value,
            CustomerId: salesOrder.CustomerId.Value,
            OrderName: salesOrder.CustomerName,
            ShippingAddress: AddressDto.MapToDto(salesOrder.ShippingAddress),
            BillingAddress: AddressDto.MapToDto(salesOrder.BillingAddress),
            Payment: PaymentDto.MapToDto(salesOrder.Payment),
            OrderItems: salesOrder.SalesOrderItems.Select(oi => new OrderItemDto(oi.ProductId.Value,oi.ProductName,oi.Quantity,oi.Price)));
    }
}

public record SalesOrderDto(
    Guid OrderId,
    Guid CustomerId,
    string OrderName,
    AddressDto ShippingAddress,
    AddressDto BillingAddress,
    PaymentDto Payment,
    IEnumerable<OrderItemDto> OrderItems);


public record SalesOrderRequestDto(
    Guid CustomerId,
    string OrderName,
    AddressDto ShippingAddress,
    AddressDto BillingAddress,
    PaymentDto Payment,
    IEnumerable<OrderItemDto> OrderItems);

public record SalesOrderRequestUpdateDto(string OrderStatus);



