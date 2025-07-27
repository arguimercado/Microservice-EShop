namespace Order.Application.Orders.Commands.Validators;

public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.OrderRequest.CustomerId)
            .NotEmpty().WithMessage("Customer ID is required.");

        RuleFor(x => x.OrderRequest.OrderName)
            .NotEmpty().WithMessage("Order name is required.")
            .MaximumLength(100).WithMessage("Order name must not exceed 100 characters.");

        //validate shipping address
        RuleFor(x => x.OrderRequest.ShippingAddress)
            .NotNull().WithMessage("Shipping address is required.")
            .SetValidator(new AddressValidator());

        RuleFor(x => x.OrderRequest.BillingAddress)
            .NotNull().WithMessage("Billing address is required.")
            .SetValidator(new AddressValidator());

       
    }
}
