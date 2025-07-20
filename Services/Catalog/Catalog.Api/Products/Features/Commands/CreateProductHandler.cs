using Catalog.Api.Products.Models;


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


internal class CreateProductHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResponse>
{
    
    public async  Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var newProduct = Product.Create(request.Request.Name,request.Request.Description,request.Request.ImageFile, request.Request.Price);
        
        newProduct.AddCategoryRange(request.Request.Categories ?? new List<string>());
        
        //save 
        session.Store(newProduct);
        await session.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(new CreateProductResponse(newProduct.Id));
    }
}
