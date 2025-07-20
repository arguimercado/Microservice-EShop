using Basket.API.Baskets.Features.Queries;
using BuildingBlocks.Commons.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Baskets.Endpoints;

public class BasketEndpoint : BaseModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var appGroup = app.MapGroup("/api/baskets")
           .WithTags("Baskets")
           .WithOpenApi();

        appGroup.MapGet("/{username}", async ([FromRoute] string username, ISender sender) =>
        {
            var query = new GetBasketQuery(username);
            var result = await sender.Send(query);

            return HandleResults(result);
        }).WithName("Get Basket");
    }
    
}
