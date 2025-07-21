using Basket.API.Baskets.Contracts;
using Basket.API.Baskets.Models;
using BuildingBlocks.Commons.Errors;

namespace Basket.API.Baskets.Features.Queries;

public record GetBasketQuery(string UserName) : IQuery<ShoppingCart>;

internal class GetBasketQueryHandler(IBasketRepository basketRepository) : IQueryHandler<GetBasketQuery, ShoppingCart>
{
    public async Task<Result<ShoppingCart>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var basket = await basketRepository.GetBasketAsync(request.UserName, cancellationToken);
        if(basket == null) {
            return Result.Fail(new NotFoundErrorResult($"Basket for user {request.UserName} not found."));
        }

        return Result.Ok(basket);
    }
}


