using BuildingBlocks.Messaging.Basket.Events;
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
            OrderItems: salesOrder.SalesOrderItems.Select(oi => new OrderItemDto( oi.ProductId.Value,oi.ProductName,oi.Quantity,oi.Price)));
    }

    public static SalesOrderRequestDto MapEventToRequest(BasketCheckoutEvent checkoutEvent)
    {
        var orderEvent = checkoutEvent.OrderEvent;
        
        var addressDto = new AddressDto(orderEvent.FirstName,
           orderEvent.LastName,
           orderEvent.EmailAddress,
           orderEvent.AddressLine,
           orderEvent.City,
           orderEvent.Country,
           orderEvent.State,
           orderEvent.ZipCode);

        var paymentDto = new PaymentDto(orderEvent.CardName,
           orderEvent.CardNumber,
           DateTime.Parse(orderEvent.Expiration),
           int.Parse(orderEvent.CVV),
           orderEvent.PaymentMethod);

        var items = checkoutEvent.OrderItems.Select(item => new OrderItemDto(
           ProductId: item.ProductId,
           ProductName: item.ProductName,
           Quantity: item.Quantity,
           Price: item.Price));

        return new SalesOrderRequestDto(
            CustomerId: orderEvent.CustomerId,
            OrderName: orderEvent.UserName,
            ShippingAddress: addressDto,
            BillingAddress: addressDto,
            Payment: paymentDto,
            OrderItems: items);
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



