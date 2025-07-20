
using Catalog.Api.Products.Models;


namespace Catalog.Api.Products.Features.Commands;

public record UpdateProductRequest(string Name, string Description, decimal Price, string? ImageFile);
public record UpdateProductCommand(Guid Id, UpdateProductRequest Request) : ICommand;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required.");
        RuleFor(x => x.Request.Name).NotNull().NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Request.Description).NotNull().NotEmpty().WithMessage("Description is required.");
        RuleFor(x => x.Request.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
    }
}

internal class UpdateProductHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand>
{
    public async Task<Result<Unit>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {

        var existingProduct = await session.LoadAsync<Product>(request.Id, cancellationToken);

        if (existingProduct == null)
        {
            throw new KeyNotFoundException($"Product with Id {request.Id} not found.");
        }

        existingProduct.Update(request.Request.Name, request.Request.Description, request.Request.ImageFile, request.Request.Price);

        session.Update(existingProduct);

        await session.SaveChangesAsync(cancellationToken);

        return Result.Ok(Unit.Value);
    }
}
