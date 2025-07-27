
using Order.Domain.CustomerDomain.Types;
using Order.Domain.ProductDomain.Types;
using Order.Domain.SalesOrderDomain.Models;

namespace Order.Application.Orders.Commands;

public record CreateOrderResult(string OrderId);

public record CreateOrderCommand(SalesOrderRequestDto OrderRequest) : ICommand<CreateOrderResult>;

internal class CreateOrderCommandHandler(
    ISalesOrderRepository repository,
    IUnitWork unitWork) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{

    public async Task<Result<CreateOrderResult>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {   

        var shippingAddress = AddressDto.MapToDomain(request.OrderRequest.ShippingAddress);
        var billingAddress = AddressDto.MapToDomain(request.OrderRequest.BillingAddress);

        var newOrder = SalesOrder.Create(
                customerId: CustomerId.Of(request.OrderRequest.CustomerId),
                customerName: request.OrderRequest.OrderName,
                shippingAddress: shippingAddress,
                billingAddress: billingAddress)
            .AddPayment(
                cardName: request.OrderRequest.Payment.CardName ?? "",
                cardNumber: request.OrderRequest.Payment.CardNumber,
                expiration: request.OrderRequest.Payment.Expiration,
                cvv: request.OrderRequest.Payment.CVV,
                method: request.OrderRequest.Payment.PaymentMethod);


       
        foreach(var item in request.OrderRequest.OrderItems)
        {   
            newOrder.AddOrderItem(ProductId.Of(item.ProductId),item.ProductName, item.Quantity,item.Price);
        }

        repository.Add(newOrder);

        await unitWork.CommitChangesAsync(cancellationToken);

        return Result.Ok(new CreateOrderResult(newOrder.Id.Value.ToString()));
    }
}