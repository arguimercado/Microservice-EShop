
using Catalog.Api.Products.Models;

namespace Catalog.Api.Products.Features.Commands;

public record UpdateProductRequest(string Name, string Description, decimal Price, string? ImageFile);
public record UpdateProductCommand(Guid Id, UpdateProductRequest Request) : ICommand;
internal class UpdateProductHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand>
{
    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await session.LoadAsync<Product>(request.Id, cancellationToken);

        if (existingProduct == null) {
            throw new KeyNotFoundException($"Product with Id {request.Id} not found.");
        }

        existingProduct.Update(request.Request.Name, request.Request.Description, request.Request.ImageFile, request.Request.Price);

        session.Update(existingProduct);

        await session.SaveChangesAsync(cancellationToken);

        return Unit.Value;

    }
}
