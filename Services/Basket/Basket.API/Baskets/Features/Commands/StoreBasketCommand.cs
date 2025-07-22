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

internal class StoreBasketCommandHandler(
    IBasketRepository basketRepository,
    IDiscountService discountService) : ICommandHandler<StoreBasketCommand>
{
    public async Task<Result<Unit>> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {

        ShoppingCart cart = ShoppingCart.Create(request.Cart.UserName);

        var itemsTask = request.Cart.Items.Select(async m => new ShoppingCartItem
        {
            Color = m.Color,
            Price = await discountService.CalculateDiscountPrice(m.ProductId.ToString(), m.Price)
                            .ConfigureAwait(false),
            ProductId = m.ProductId,
            ProductName = m.ProductName,
            Quantity = m.Quantity
        });

        var items = await Task.WhenAll(itemsTask);

        cart.UpdateItems(items);

        await basketRepository.StoreBasketAsync(cart, cancellationToken);
            

        return await Task.FromResult(Result.Ok(Unit.Value));
    }
}

