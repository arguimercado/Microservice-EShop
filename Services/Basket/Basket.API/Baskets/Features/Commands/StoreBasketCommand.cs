using Basket.API.Baskets.Contracts;
using Basket.API.Baskets.Models;

namespace Basket.API.Baskets.Features.Commands;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand;

public class StoreBasketValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketValidator()
    {
        RuleFor(x => x.Cart)
            .NotNull()
            .WithMessage("Shopping cart cannot be null.");
        RuleFor(x => x.Cart.UserName)
            .NotEmpty()
            .WithMessage("Username is required.");
       
    }
}

internal class StoreBasketCommandHandler(IBasketRepository basketRepository) : ICommandHandler<StoreBasketCommand>
{
    public async Task<Result<Unit>> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        ShoppingCart cart = ShoppingCart.Create(request.Cart.UserName, request.Cart.Items);

        await basketRepository.StoreBasketAsync(cart, cancellationToken);
            

        return await Task.FromResult(Result.Ok(Unit.Value));
    }
}

