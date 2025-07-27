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

        group.MapPost("", async ([FromBody] SalesOrderRequestDto request, ISender sender) =>
        {
            var command = new CreateOrderCommand(request);
            var response = await sender.Send(command);

            return HandleResults(response);
        });

        group.MapPut("{orderId:guid}", async ([FromRoute]Guid orderId,[FromBody]SalesOrderRequestUpdateDto request, ISender sender) => {

            var command = new UpdateOrderStatusCommand(orderId, request);
            var response = await sender.Send(command);

            return HandleResults(response);
        });

        
        group.MapGet("", async ([AsParameters] PaginationRequest pagination, ISender sender) =>
        {
            var query = new GetOrdersQuery(pagination);
            var response = await sender.Send(query);
            return HandleResults(response);
        });

        group.MapGet("/{orderId:guid}", async ([FromRoute] Guid orderId, ISender sender) =>
        {
            var query = new GetSingleOrderQuery(orderId);
            var response = await sender.Send(query);

            return HandleResults(response);
        });

        group.MapGet("/{customerId:guid}/customer", async ([FromRoute] Guid customerId, ISender sender) =>
        {
            var query = new GetOrdersByCustomerQuery(customerId);
            var response = await sender.Send(query);

            return HandleResults(response);
        });
    }
}
