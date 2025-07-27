using DomainBlocks.Commons.Options;
using Order.Application.Orders.Commands;
using Order.Application.Orders.Dtos;
using Order.Application.Orders.Queries;

namespace Order.API.Endpoints;

public class SalesOrderEndpoint : BaseModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/orders");


        group.MapPost("", async ([FromBody]SalesOrderRequestDto request,ISender sender) =>
        {
            var command = new CreateOrderCommand(request);
            var response = await sender.Send(command);


            return HandleResults(response);
        });
        group.MapGet("", async ([AsParameters]PaginationRequest pagination,ISender sender) =>
        {
            var query = new GetOrdersQuery(pagination);
            var response = await sender.Send(query);
            return HandleResults(response);
        });
    }
}
