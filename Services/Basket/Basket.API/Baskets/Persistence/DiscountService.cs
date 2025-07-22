using Basket.API.Baskets.Contracts;
using Discount.Grpc;

namespace Basket.API.Baskets.Persistence;

internal class DiscountService(DiscountProtoService.DiscountProtoServiceClient discountProto) : IDiscountService
{
    public async Task<decimal> CalculateDiscountPrice(string productId, decimal price)
    {
        var discount = await discountProto.GetDiscountAsync(new GetDiscountRequest() { Id = productId });
        return price - (decimal)discount.Amount; // Explicitly cast float to decimal
    }
}
