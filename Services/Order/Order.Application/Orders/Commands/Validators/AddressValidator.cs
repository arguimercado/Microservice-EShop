namespace Order.Application.Orders.Commands.Validators;

public class AddressValidator : AbstractValidator<AddressDto>
{
    public AddressValidator()
    {
        RuleFor(x => x.FirstName)
            .NotNull()
            .NotEmpty().WithMessage("Firstname is required.");
        RuleFor(x => x.LastName)
            .NotNull()
            .NotEmpty().WithMessage("Lastname is required.");


        RuleFor(x => x.EmailAddress)
            .NotNull()
            .NotEmpty()
            .WithMessage("Email Address is required.");

        RuleFor(x => x.AddressLine)
            .NotEmpty().WithMessage("Address line is required.")
            .MaximumLength(200).WithMessage("Address line must not exceed 200 characters.");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required.")
            .MaximumLength(100).WithMessage("Country must not exceed 100 characters.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(100).WithMessage("City must not exceed 100 characters.");
        RuleFor(x => x.State)
            .NotEmpty().WithMessage("State is required.")
            .MaximumLength(50).WithMessage("State must not exceed 50 characters.");
        RuleFor(x => x.ZipCode)
            .NotEmpty().WithMessage("Zip code is required.")
            .Matches(@"^\d{5}(-\d{4})?$").WithMessage("Zip code must be a valid format.");
    }
}

