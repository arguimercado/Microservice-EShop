using Basket.API.Baskets.Contracts;
using BuildingBlocks.Commons.Errors;

namespace Basket.API.Baskets.Features.Commands;

public record DeleteBasketCommand(string UserName) : ICommand;

internal class DeleteBasketCommandHandler(IBasketRepository basketRepository) : ICommandHandler<DeleteBasketCommand>
{
    public async Task<Result<Unit>> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        var currentBasket = await basketRepository.GetBasketAsync(request.UserName, cancellationToken);

        if (currentBasket == null)
            return Result.Fail(new NotFoundErrorResult($"User {request.UserName} Basket doesn't exist"));

        await basketRepository.DeleteBasketAsync(currentBasket, cancellationToken);

        return await Task.FromResult(Result.Ok(Unit.Value));
    }
}

