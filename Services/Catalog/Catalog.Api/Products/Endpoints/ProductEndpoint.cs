
using Catalog.Api.Commons.Abstracts;
using Catalog.Api.Products.Features.Commands;
using Catalog.Api.Products.Features.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Products.Endpoints;

public class ProductEndpoint : BaseModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/products")
            .WithName("Products")
            .WithOpenApi();


        group.MapPost("", async (ISender sender, CreateProductRequest request) =>
        {
            var command = new CreateProductCommand(request);
            var response = await sender.Send(command);

            return HandleResults(response);
        }).WithName("Create Product");

        group.MapPut("/{id:guid}", async (ISender sender,[FromRoute]Guid id, [FromBody]UpdateProductRequest request) =>
        {
            var command = new UpdateProductCommand(id,request);
            var response = await sender.Send(command);

            return HandleResults(response);
        }).WithName("Update Product");

        group.MapGet("", async (ISender sender) =>
        {
            var query = new GetProductsQuery();
            var response = await sender.Send(query);

            return HandleResults(response);
        }).WithName("Get Products");

        group.MapGet("/{id:Guid}", async (ISender sender, [FromRoute] Guid id) =>
        {
            var query = new GetProductByIdQuery(id);
            var response = await sender.Send(query);
            return HandleResults(response);
        }).WithName("Get Product");

        group.MapGet("/{category}/product", async (ISender sender, [FromRoute] string category) =>
        {
            var query = new GetProductByCategoryQuery(category);
            var response = await sender.Send(query);

            return HandleResults(response);
        }).WithName("Get By Category");
    }
}
