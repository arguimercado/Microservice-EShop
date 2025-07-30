using Basket.API.Baskets.Contracts;
using Basket.API.Baskets.Dtos;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Baskets.Features.Commands;


public record CheckoutBasketRequest(BasketCheckoutDto BasketCheckoutRequest);

public record CheckoutBasketResponse(bool IsSuccess);

public record CheckoutBasketCommand(CheckoutBasketRequest Request) : ICommand<CheckoutBasketResponse>;

public class CheckoutBasketValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketValidator()
    {
        RuleFor(x => x.Request.BasketCheckoutRequest.UserName)
            .NotNull()
            .NotEmpty().WithMessage("UserName is required.");

        RuleFor(x => x.Request.BasketCheckoutRequest.CustomerId)
            .NotNull()
            .NotEmpty().WithMessage("CustomerId is required.");
        
        RuleFor(x => x.Request.BasketCheckoutRequest.FirstName)
            .NotNull()
            .NotEmpty().WithMessage("FirstName is required.");

        RuleFor(x => x.Request.BasketCheckoutRequest.LastName)
            .NotNull()
            .NotEmpty().WithMessage("LastName is required.");
        
        RuleFor(x => x.Request.BasketCheckoutRequest.EmailAddress)
            .NotNull()
            .NotEmpty().WithMessage("EmailAddress is required.")
            .EmailAddress().WithMessage("EmailAddress must be a valid email format.");

        RuleFor(x => x.Request.BasketCheckoutRequest.AddressLine)
            .NotNull()
            .NotEmpty().WithMessage("AddressLine is required.");

        RuleFor(x => x.Request.BasketCheckoutRequest.Country)
            .NotNull()
            .NotEmpty().WithMessage("Country is required.");

        RuleFor(x => x.Request.BasketCheckoutRequest.City)
            .NotNull()
            .NotEmpty().WithMessage("City is required.");

        RuleFor(x => x.Request.BasketCheckoutRequest.State)
            .NotNull()
            .NotEmpty().WithMessage("State is required.");

        RuleFor(x => x.Request.BasketCheckoutRequest.ZipCode)
            .NotNull()
            .NotEmpty().WithMessage("ZipCode is required.");

    }
}


internal class CheckoutBasketCommandHandler(IBasketRepository basketRepository,
    IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResponse>
{

    public async Task<Result<CheckoutBasketResponse>> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
    {
        //get existing basket with total price
        //set total price on basket checkout event message
        //send basket checkout event with rabbitmq masstransit
        //delete the basket
        var basketRequest = request.Request.BasketCheckoutRequest;

        var basket = await basketRepository.GetBasketAsync(basketRequest.UserName, cancellationToken);
        
        if (basket is null) {
            return Result.Ok(new CheckoutBasketResponse(false));
        }

        var eventMessage = new BasketCheckoutEvent
        {
            UserName = basketRequest.UserName,
            CustomerId = basketRequest.CustomerId,
            TotalPrice = basket.TotalPrice,
            FirstName = basketRequest.FirstName,
            LastName = basketRequest.LastName,
            EmailAddress = basketRequest.EmailAddress,
            AddressLine = basketRequest.AddressLine,
            Country = basketRequest.Country,
            City = basketRequest.City,
            State = basketRequest.State,
            ZipCode = basketRequest.ZipCode,
            CardName = basketRequest.CardName,
            CardNumber = basketRequest.CardNumber,
            Expiration = basketRequest.Expiration,
            CVV = basketRequest.CVV,
            PaymentMethod = basketRequest.PaymentMethod
        };

        await publishEndpoint.Publish(eventMessage, cancellationToken);
        await basketRepository.DeleteBasketAsync(basket, cancellationToken);

        return Result.Ok(new CheckoutBasketResponse(true));

    }
}

