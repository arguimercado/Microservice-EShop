using Catalog.Api.Products.Dtos;
using Catalog.Api.Products.Models;

namespace Catalog.Api.Products.Features.Queries;


public record GetProductByCategoryResponse(IEnumerable<ProductDto> Products);
public record GetProductByCategoryQuery(string category) : IQuery<GetProductByCategoryResponse>;
internal class GetProductByCategoryHandler(IDocumentSession session) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResponse>
{
    
    public async Task<GetProductByCategoryResponse> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
    {
        var results = await session.Query<Product>()
                                .Where(p => p.Categories.Contains(request.category))
                                .ToListAsync();


        return new GetProductByCategoryResponse(results.Select(p => new ProductDto(p)));

    }
}
