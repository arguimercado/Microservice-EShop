using Basket.API.Baskets.Contracts;
using Basket.API.Baskets.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Baskets.Persistence;

public class CachedBasketRepository(IBasketRepository basketRepository, IDistributedCache distributedCache) : IBasketRepository
{
    public async Task DeleteBasketAsync(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        await basketRepository.DeleteBasketAsync(cart, cancellationToken);
        await distributedCache.RemoveAsync(cart.UserName, cancellationToken);   
    }

    public async Task<ShoppingCart?> GetBasketAsync(string username, CancellationToken cancellationToken = default)
    {
        var cachedBasket = await distributedCache.GetStringAsync(username, cancellationToken);
        if (!string.IsNullOrEmpty(cachedBasket)) {
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket);
        }

        var basket = await basketRepository.GetBasketAsync(username, cancellationToken);
        await distributedCache.SetStringAsync(username,JsonSerializer.Serialize(basket),cancellationToken);

        return basket;
    }

    public async Task StoreBasketAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
    {
        await basketRepository.StoreBasketAsync(shoppingCart, cancellationToken);
        await distributedCache.SetStringAsync(shoppingCart.UserName, JsonSerializer.Serialize(shoppingCart), cancellationToken);
    }
}
