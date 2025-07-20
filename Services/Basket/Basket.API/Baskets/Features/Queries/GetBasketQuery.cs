using Basket.API.Baskets.Models;

namespace Basket.API.Baskets.Features.Queries;




public record GetBasketQuery(string UserName) : IQuery<ShoppingCart>;


internal class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, ShoppingCart>
{

    public async Task<Result<ShoppingCart>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(Result.Ok(new ShoppingCart("user")));
    }
}


