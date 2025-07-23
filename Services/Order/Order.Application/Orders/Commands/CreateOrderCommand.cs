using Order.Application.Commons.Contracts;
using Order.Application.Orders.Contracts;
using Order.Domain.SalesOrderDomain.Models;

namespace Order.Application.Orders.Commands;

public record CreateOrderResult(string OrderId);

public record CreateOrderCommand(SalesOrderDto OrderRequest) : ICommand<CreateOrderResult>;

public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.OrderRequest.CustomerId)
            .NotEmpty().WithMessage("Customer ID is required.");

        RuleFor(x => x.OrderRequest.OrderName)
            .NotEmpty().WithMessage("Order name is required.")
            .MaximumLength(100).WithMessage("Order name must not exceed 100 characters.");

        RuleFor(x => x.OrderRequest.OrderItems).Empty()
            .WithMessage("Order items must be empty when creating a new order.");
    }
}


internal class CreateOrderCommandHandler(
    ISalesOrderRepository repository,
    IUnitWork unitWork) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{

    public async Task<Result<CreateOrderResult>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {

        

        var shippingAddress = AddressDto.MapToDomain(request.OrderRequest.ShippingAddress);
        var billingAddress = AddressDto.MapToDomain(request.OrderRequest.BillingAddress);
        
        
        var newOrder = SalesOrder.Create(
                customerId: request.OrderRequest.CustomerId,
                customerName: request.OrderRequest.OrderName,
                shippingAddress: shippingAddress,
                billingAddress: billingAddress)
            .AddPayment(
                cardName: request.OrderRequest.Payment.CardName ?? "",
                cardNumber: request.OrderRequest.Payment.CardNumber,
                expiration: request.OrderRequest.Payment.Expiration,
                cvv: request.OrderRequest.Payment.CVV,
                method: request.OrderRequest.Payment.PaymentMethod);

        repository.Add(newOrder);

        await unitWork.CommitSaveChangesAsync(cancellationToken);

        return Result.Ok(new CreateOrderResult(newOrder.Id.Value.ToString()));
    }
}