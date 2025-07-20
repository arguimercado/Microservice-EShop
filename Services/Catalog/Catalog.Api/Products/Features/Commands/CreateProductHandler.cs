using Catalog.Api.Products.Models;
using FluentResults;

namespace Catalog.Api.Products.Features.Commands;



public record CreateProductRequest(
    string Name,
    string Description,
    string? ImageFile,
    decimal Price,
    List<string> Categories = default!
);

public record CreateProductResponse(Guid Id);

public record CreateProductCommand(CreateProductRequest Request) : ICommand<CreateProductResponse>;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Request.Name).NotNull().NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Request.Description).NotNull().NotEmpty().WithMessage("Description is required.");
        RuleFor(x => x.Request.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
        RuleFor(x => x.Request.Categories).NotNull().WithMessage("Categories cannot be null.");
    }
}


internal class CreateProductHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResponse>
{
    
    public async Task<Result<CreateProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var newProduct = Product.Create(request.Request.Name, request.Request.Description, request.Request.ImageFile, request.Request.Price);

        newProduct.AddCategoryRange(request.Request.Categories ?? new List<string>());

        //save 
        session.Store(newProduct);
        await session.SaveChangesAsync(cancellationToken);

        return Result.Ok(new CreateProductResponse(newProduct.Id));
    }
}
