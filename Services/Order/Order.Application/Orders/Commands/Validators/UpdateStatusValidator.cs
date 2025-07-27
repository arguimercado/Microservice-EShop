namespace Order.Application.Orders.Commands.Validators;

public class UpdateStatusValidator : AbstractValidator<UpdateOrderStatusCommand>
{
    public UpdateStatusValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("Order Id is required")
            .NotEqual(Guid.Empty).WithMessage("Order Id cannot be empty");
        
        RuleFor(x => x.Request.OrderStatus)
            .NotEmpty().WithMessage("Order status is required")
            .Must(status => Enum.TryParse<OrderStatusEnum>(status, out _))
            .WithMessage("Invalid order status");
    }
}
