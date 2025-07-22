namespace Basket.API.Baskets.Contracts;

public interface IDiscountService
{
    Task<decimal> CalculateDiscountPrice(string productId,decimal price);
    
}
