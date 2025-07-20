using Basket.API.Baskets.Features.Commands;
using Basket.API.Baskets.Features.Queries;
using Basket.API.Baskets.Models;
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

        appGroup.MapPost("", async ([FromBody] ShoppingCart cart, ISender sender) =>
        {
            var command = new StoreBasketCommand(cart);
            var result = await sender.Send(command);

            return HandleResults(result);

        }).WithName("Create Basket")
        .WithDescription("Create a new shopping cart for the user.")
        .WithDisplayName("CreateBasket");
    }
    
}
