using Catalog.Api.Products.Dtos;
using Catalog.Api.Products.Models;
using Marten.Pagination;

namespace Catalog.Api.Products.Features.Queries;

public record GetProductsResponse(IEnumerable<ProductDto> Products);

public record GetProductsRequest(int? PageNumber = 1,int? PageSize = 10);

public record GetProductsQuery(GetProductsRequest Request) : IQuery<GetProductsResponse>;

internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger)
    : IQueryHandler<GetProductsQuery, GetProductsResponse>
{
    public async Task<Result<GetProductsResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get Products Query Handler Invoked");
        var products = await session
                                .Query<Product>()
                                .ToPagedListAsync(request.Request.PageNumber ?? 1, request.Request.PageSize ?? 10, cancellationToken);

        return Result.Ok(new GetProductsResponse(products.Select(p => new ProductDto(p))));
    }
}
