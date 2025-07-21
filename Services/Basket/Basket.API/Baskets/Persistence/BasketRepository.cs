using Basket.API.Baskets.Contracts;
using Basket.API.Baskets.Models;


namespace Basket.API.Baskets.Persistence;

public class BasketRepository(IDocumentSession session) : IBasketRepository
{
    public async Task DeleteBasketAsync(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        
        session.Delete<ShoppingCart>(cart);
        await session.SaveChangesAsync(cancellationToken);
        
    }

    public async Task<ShoppingCart?> GetBasketAsync(string username, CancellationToken cancellationToken = default)
    {
        var basket = await session.Query<ShoppingCart>().FirstOrDefaultAsync(c => c.UserName == username, cancellationToken);

        return basket;
    }

    public async Task StoreBasketAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
    {
        session.Store(shoppingCart);
        await session.SaveChangesAsync(cancellationToken);
    }
}
