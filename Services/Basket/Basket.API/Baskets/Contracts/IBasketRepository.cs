using Basket.API.Baskets.Models;

namespace Basket.API.Baskets.Contracts;

public interface IBasketRepository
{
    Task<ShoppingCart?> GetBasketAsync(string username,CancellationToken cancellationToken = default);
    Task StoreBasketAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken = default);
    Task DeleteBasketAsync(ShoppingCart cart, CancellationToken cancellationToken = default);

}
