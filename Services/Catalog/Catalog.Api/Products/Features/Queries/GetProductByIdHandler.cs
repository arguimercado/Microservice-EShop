using Catalog.Api.Products.Dtos;
using Catalog.Api.Products.Models;

namespace Catalog.Api.Products.Features.Queries;

public record GetProductByIdQuery(Guid Id) : IQuery<ProductDto>;

internal class GetProductByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IDocumentSession _session = session;
   

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {  
        var product = await _session.LoadAsync<Product>(request.Id, cancellationToken);
        if (product == null) {
            throw new KeyNotFoundException($"Product with Id {request.Id} not found.");
        }
        return new ProductDto(product);
    }
}

