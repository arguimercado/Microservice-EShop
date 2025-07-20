using Catalog.Api.Products.Dtos;
using Catalog.Api.Products.Models;

namespace Catalog.Api.Products.Features.Queries;

public record ProductResponse(IEnumerable<ProductDto> Products);

public record GetProductsQuery : IQuery<ProductResponse>;

internal class GetProductsQueryHandler(IDocumentSession session,ILogger<GetProductsQueryHandler> logger)
    : IQueryHandler<GetProductsQuery, ProductResponse>
{
    public async Task<ProductResponse> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get Products Query Handler Invoked");
        var products = await session.Query<Product>().ToListAsync(cancellationToken);

        return new ProductResponse(products.Select(p => new ProductDto(p)));
    }
}
